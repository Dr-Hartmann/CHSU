import numpy as np
import pandas as pd
import torch
import tqdm
from sklearn.ensemble import GradientBoostingRegressor
from sklearn.linear_model import LinearRegression
from sklearn.tree import DecisionTreeRegressor
from torch import nn, optim

from myutils import fix_seeds

cat_cols = ["sex", "region", "smoker"]
num_cols = ["age", "bmi", "charges"]


def main():
    fix_seeds(42)

    df = pd.read_csv("insurance.csv").dropna()
    df[num_cols] = df[num_cols].astype(np.float32)

    df_train, df_test = split(df.copy())
    df_train, df_test = one_hot_code(df_train.copy(), df_test.copy())
    df_train, df_test = normalization(df_train.copy(), df_test.copy())
    print(df_train, df_test)

    X_train = df_train.copy()
    y_train = X_train.pop("charges")
    X_test = df_test.copy()
    y_test = X_test.pop("charges")

    test_pred_mse(LinearRegression(), X_train, y_train, X_test, y_test)
    test_pred_mse(GradientBoostingRegressor(), X_train, y_train, X_test, y_test)

    x_t_np = X_train.to_numpy(dtype=np.float32)
    y_t_np = y_train.to_numpy(dtype=np.float32)
    x_test_np = torch.from_numpy(X_test.to_numpy(dtype=np.float32))

    losses1, model = test_torch_pred(build_model_1(), x_t_np, y_t_np)
    print(mse(model(x_test_np), y_test))

    losses2, model = test_torch_pred(build_model_2(), x_t_np, y_t_np)
    print(mse(model(x_test_np), y_test))

    losses3, model = test_torch_pred(build_model_3(), x_t_np, y_t_np)
    print(mse(model(x_test_np), y_test))


def split(df: pd.DataFrame, seed: int = 42) -> tuple[pd.DataFrame, pd.DataFrame]:
    """
    1 - лучше, но `sklearn.model_selection.train_test_split` проще в обращении
    """
    rng = np.random.default_rng(seed)
    test_idx = rng.choice(df.index, size=int(df.shape[0] * 0.2), replace=False)
    train_idx = df.index.difference(test_idx)

    """
    2 - устарел и зависит от внешнего `np.random.seed()`
    """
    # test_idx = np.random.choice(df.index, size=int(len(df) * 0.2), replace=False)
    # train_idx = df.index.difference(test_idx)

    return df.iloc[train_idx], df.iloc[test_idx]


def one_hot_code(
    df_train: pd.DataFrame, df_test: pd.DataFrame
) -> tuple[pd.DataFrame, pd.DataFrame]:
    df_train = pd.get_dummies(df_train, columns=cat_cols, prefix=cat_cols)
    df_test = pd.get_dummies(df_test, columns=cat_cols, prefix=cat_cols)
    return df_train.align(df_test, join="left", axis=1, fill_value=0)


def normalization(
    df_train: pd.DataFrame, df_test: pd.DataFrame
) -> tuple[pd.DataFrame, pd.DataFrame]:
    for col in num_cols:
        min_v, max_v = df_train[col].min(), df_train[col].max()
        df_train[col] = (df_train[col] - min_v) / (max_v - min_v)
        df_test[col] = (df_test[col] - min_v) / (max_v - min_v)

        # mean = df_train[col].mean()
        # std = df_train[col].std(ddof=0)
        # df_train[col] = (df_train[col] - mean) / std
        # df_test[col] = (df_test[col] - mean) / std
        # df_train[col] = df_train[col] / df_train[col].std()

    return df_train, df_test


def test_torch_pred(
    model: nn.Module,
    X_train: np.ndarray,
    y_train: np.ndarray,
) -> tuple[list[float], nn.Module]:
    crit = nn.MSELoss()
    optm = optim.Adam(
        model.parameters(),
        lr=1e-2,
        weight_decay=1e-5,
    )

    x = torch.from_numpy(X_train).float()
    y = torch.from_numpy(y_train).float().unsqueeze(1)

    model.train()
    losses: list[float] = []

    for _ in tqdm.trange(2000):
        optm.zero_grad()
        pred: torch.Tensor = model(x)
        loss: torch.Tensor = crit(pred, y)
        loss.backward()
        optm.step()
        losses.append(loss.item())

    return losses, model


def mse(
    y: torch.Tensor | pd.Series | np.ndarray,
    preds: torch.Tensor | pd.Series | np.ndarray,
) -> float:
    if torch.is_tensor(preds):
        preds = preds.detach().numpy()
    elif isinstance(preds, pd.Series):
        preds = preds.values
    preds = np.asarray(preds)

    if torch.is_tensor(y):
        y = y.detach().numpy()
    elif isinstance(y, pd.Series):
        y = y.values
    y = np.asarray(y)

    return np.mean((y - preds) ** 2)


def test_pred_mse(
    model: DecisionTreeRegressor,
    X_train: pd.DataFrame,
    y_train: pd.DataFrame,
    X_test: pd.DataFrame,
    y_test: pd.DataFrame,
) -> None:
    model.fit(X_train, y_train)
    pred = model.predict(X_test)
    MSE = mse(y_test, pred)
    print(MSE)


def build_model_1() -> nn.Module:
    return nn.Linear(11, 1)


def build_model_2() -> nn.Module:
    return nn.Sequential(nn.Linear(11, 6), nn.ReLU(), nn.Linear(6, 1))


def build_model_3() -> nn.Module:
    return nn.Sequential(
        nn.Linear(11, 6), nn.ReLU(), nn.Linear(6, 6), nn.ReLU(), nn.Linear(6, 1)
    )


if __name__ == "__main__":
    main()
