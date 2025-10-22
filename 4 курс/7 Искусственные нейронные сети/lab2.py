import albumentations as A
import matplotlib.pyplot as plt
import numpy as np
import torch
import tqdm
from IPython.display import clear_output
from torch import nn, optim
from torch.utils.data import DataLoader
from torchvision.datasets import CIFAR10

from myutils import fix_seeds, TrainConfig





def main():
    params = TrainConfig(lr=1e-3, num_epochs=4)
    fix_seeds(params.seed)

    train_loader, test_loader = get_cifar_loaders(params.batch_size)
    model = SimpleCNNModel().to(params.device)
    model.fit(params, train_loader)
    model.accuracy(params, test_loader)

    report_parameters(model)


class SimpleCNNModel(nn.Module):
    def __init__(self, num_classes=10):
        super().__init__()
        self.crit = nn.CrossEntropyLoss()
        self.conv_net = nn.Sequential(
            # 1
            nn.Conv2d(3, 64, kernel_size=3, padding=1),
            nn.ReLU(inplace=True),
            nn.MaxPool2d(kernel_size=2),
            # 2
            nn.Conv2d(64, 128, kernel_size=3, padding=1),
            nn.ReLU(inplace=True),
            nn.MaxPool2d(kernel_size=2),
            # 3
            nn.Conv2d(128, 256, kernel_size=3, padding=1),
            nn.ReLU(inplace=True),
            nn.MaxPool2d(kernel_size=2),
        )
        self.fc = nn.Sequential(
            # 1
            nn.Dropout(0.5),
            nn.Linear(256 * 4 * 4, 256),
            nn.ReLU(inplace=True),
            # 2
            nn.Dropout(0.5),
            nn.Linear(256, num_classes),
        )

    def forward(self, x: torch.Tensor):
        x = self.conv_net(x)
        x = x.reshape((x.shape[0], -1))
        x = self.fc(x)
        return x

    def fit(self, cfg: TrainConfig, trn_ldr: DataLoader):
        optm = optim.Adam(self.parameters(), lr=cfg.lr, weight_decay=cfg.weight_decay)
        self.to(cfg.device) 
        super().train(True)
        for epoch in range(cfg.num_epochs):
            print(f"Epoch ( {epoch} )")
            losses: list[float] = []
            for i, (x_batch, y_batch) in enumerate(tqdm.tqdm(trn_ldr)):
                optm.zero_grad()
                x_batch: torch.Tensor = x_batch.to(cfg.device)
                y_batch: torch.Tensor = y_batch.to(cfg.device)
                pred: torch.Tensor = self(x_batch)
                loss: torch.Tensor = self.crit(pred, y_batch)
                loss.backward()
                optm.step()
                losses.append(loss.item())

            print(f"Losses: {sum(losses)}")
            # self.plot_losses(epoch, losses)

    def plot_losses(self, epoch: int, values: list[float]):
        clear_output(wait=False)
        plt.title(f"Epoch {epoch}. Accuracy: {values[-1]:.4f}")
        plt.xlabel("Epoch")
        plt.ylabel("Loss")
        plt.plot(values)
        plt.grid(True)
        plt.show()

    def accuracy(self, cfg: TrainConfig, val_ldr: DataLoader):
        correct = 0
        total = 0

        super().eval()
        with torch.no_grad():
            for i, (x_batch, y_batch) in enumerate(tqdm.tqdm(val_ldr)):
                x_batch: torch.Tensor = x_batch.to(cfg.device)
                y_batch: torch.Tensor = y_batch.to(cfg.device)

                logits: torch.Tensor = self(x_batch)
                preds = logits.argmax(dim=1)

                correct += (preds == y_batch).sum().item()
                total += y_batch.size(0)

        acc = correct / total
        print(f"Accuracy: {acc:.4f}")


def get_cifar_loaders(batch_size=256):
    train_dataset = CIFAR10(
        root="./data",
        train=True,
        download=True,
        transform=AlbumentationsWrapper(
            A.Compose(
                [
                    A.HorizontalFlip(p=0.3),
                    A.Rotate(limit=30, p=0.3),
                    A.RandomResizedCrop(
                        size=(32, 32), scale=(0.8, 1.0), ratio=(0.9, 1.1), p=0.5
                    ),
                    A.Normalize(
                        mean=(0.4914, 0.4822, 0.4465), std=(0.2470, 0.2435, 0.2616)
                    ),
                    A.ToTensorV2(),
                ]
            )
        ),
    )
    test_dataset = CIFAR10(
        root="./data",
        train=False,
        download=True,
        transform=AlbumentationsWrapper(
            A.Compose(
                [
                    A.Normalize(
                        mean=(0.4914, 0.4822, 0.4465), std=(0.2470, 0.2435, 0.2616)
                    ),
                    A.ToTensorV2(),
                ]
            )
        ),
    )
    return (
        DataLoader(train_dataset, batch_size, shuffle=True, num_workers=0),
        DataLoader(test_dataset, batch_size, shuffle=False, num_workers=0),
    )


def report_parameters(model: nn.Module):
    print(
        "Суммарное количество параметров:",
        sum(p.nelement() for p in model.parameters()),
    )
    print(
        "Суммарный размер (Мб) параметров:",
        sum(p.nelement() * p.element_size() for p in model.parameters()) / 1024**2,
    )


class AlbumentationsWrapper:
    def __init__(self, transform):
        self.transform = transform

    def __call__(self, img):
        return self.transform(image=np.array(img))["image"]


if __name__ == "__main__":
    main()
