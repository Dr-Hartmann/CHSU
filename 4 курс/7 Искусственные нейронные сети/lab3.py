import torch
import torch.nn as nn
import torch.optim as optim
from keras.datasets import imdb
from torch.utils.data import DataLoader, TensorDataset
from tqdm import tqdm

from myutils import TrainConfig, fix_seeds

(X_train_indices, y_train), (X_test_indices, y_test) = imdb.load_data(num_words=10000)
word_to_index = {k: v - 1 + 3 for k, v in imdb.get_word_index().items()} | {
    "<PAD>": 0,
    "<START>": 1,
    "<UNK>": 2,
}
index_to_word = {index: word for word, index in word_to_index.items()}


def indices_to_words(indices):
    return " ".join(index_to_word.get(index, "<UNK>") for index in indices)


X_train = [indices_to_words(indices) for indices in X_train_indices]
X_test = [indices_to_words(indices) for indices in X_test_indices]


def main():
    case_4()

def case_4():
    pass

def case_3():
    import gensim.downloader as api
    word2vec = api.load("word2vec-google-news-300") 
    result = word2vec.most_similar(positive=['Paris', 'Germany'], negative=['France'], topn=1)
    closest_word, similarity = result[0]
    print(f"Ближайшее слово: {closest_word}")
    print(f"Косинусная близость: {similarity}")

def case_2():
    from sklearn.feature_extraction.text import CountVectorizer

    vectorizer = CountVectorizer(max_features=10000)

    x_train_bow_sparse = vectorizer.fit_transform(X_train)
    x_test_bow_sparse = vectorizer.transform(X_test)

    input_dim = len(vectorizer.get_feature_names_out()) 

    x_train_bow = torch.tensor(x_train_bow_sparse.toarray()).float()
    x_test_bow = torch.tensor(x_test_bow_sparse.toarray()).float()

    y_train_tensor = torch.tensor(y_train).float()
    y_test_tensor = torch.tensor(y_test).float()

    print(
        f"Форма полученной матрицы (количество документов, количество признаков/слов): {x_train_bow.shape}"
    )

    params = TrainConfig(lr=1e-3, num_epochs=50)
    fix_seeds(params.seed)
    model = SimpleNeuralNet(input_dim)
    model.fit(
        params,
        DataLoader(
            TensorDataset(x_train_bow, y_train_tensor),
            params.batch_size,
            shuffle=True,
            num_workers=0,
        ),
    )
    model.accuracy(
        params,
        DataLoader(
            TensorDataset(x_test_bow, y_test_tensor),
            params.batch_size,
            shuffle=False,
            num_workers=0,
        ),
    )


def case_1():
    chat_words = {
        "AFAIK": "As Far As I Know",
        "AFK": "Away From Keyboard",
        "ASAP": "As Soon As Possible",
        "ATK": "At The Keyboard",
        "ATM": "At The Moment",
        "A3": "Anytime, Anywhere, Anyplace",
        "BAK": "Back At Keyboard",
        "BBL": "Be Back Later",
        "BBS": "Be Back Soon",
        "BFN": "Bye For Now",
        "B4N": "Bye For Now",
        "BRB": "Be Right Back",
        "BRT": "Be Right There",
        "BTW": "By The Way",
        "B4": "Before",
        "CU": "See You",
        "CUL8R": "See You Later",
        "CYA": "See You",
        "FAQ": "Frequently Asked Questions",
        "FC": "Fingers Crossed",
        "FWIW": "For What It's Worth",
        "FYI": "For Your Information",
        "GAL": "Get A Life",
        "GG": "Good Game",
        "GN": "Good Night",
        "GMTA": "Great Minds Think Alike",
        "GR8": "Great!",
        "G9": "Genius",
        "IC": "I See",
        "ICQ": "I Seek you (also a chat program)",
        "ILU": "I Love You",
        "IMHO": "In My Honest/Humble Opinion",
        "IMO": "In My Opinion",
        "IOW": "In Other Words",
        "IRL": "In Real Life",
        "LDR": "Long Distance Relationship",
        "LTNS": "Long Time No See",
        "L8R": "Later",
        "MTE": "My Thoughts Exactly",
        "M8": "Mate",
        "NRN": "No Reply Necessary",
        "OIC": "Oh I See",
        "PITA": "Pain In The A..",
        "PRT": "Party",
        "PRW": "Parents Are Watching",
        "QPSA": "Que Pasa?",
        "ROFL": "Rolling On The Floor Laughing",
        "ROFLOL": "Rolling On The Floor Laughing Out Loud",
        "ROTFLMAO": "Rolling On The Floor Laughing My A.. Off",
        "SK8": "Skate",
        "STATS": "Your sex and age",
        "ASL": "Age, Sex, Location",
        "THX": "Thank You",
        "TTFN": "Ta-Ta For Now!",
        "TTYL": "Talk To You Later",
        "U2": "You Too",
        "U4E": "Yours For Ever",
        "WB": "Welcome Back",
        "WTF": "What The F...",
        "WTG": "Way To Go!",
        "WUF": "Where Are You From?",
        "W8": "Wait...",
        "7K": "Sick:-D Laughter",
        "TFW": "That feeling when",
        "MFW": "My face when",
        "MRW": "My reaction when",
        "IFYP": "I feel your pain",
        "LOL": "Laughing out loud",
        "TNTL": "Trying not to laugh",
        "JK": "Just kidding",
        "IDC": "I don’t care",
        "ILY": "I love you",
        "IMU": "I miss you",
        "ADIH": "Another day in hell",
        "ZZZ": "Sleeping, bored, tired",
        "WYWH": "Wish you were here",
        "BAE": "Before anyone else",
        "FIMH": "Forever in my heart",
        "BSAAW": "Big smile and a wink",
        "BWL": "Bursting with laughter",
        "LMAO": "Laughing my a** off",
        "BFF": "Best friends forever",
        "CSL": "Can’t stop laughing",
    }
    chat_words = {key.lower(): value.lower() for key, value in chat_words.items()}
    chat_set = set(chat_words.keys())
    count = sum(len(set(sentence.lower().split()) & chat_set) for sentence in X_test)
    print("Количество сокращений в тестовом наборе:", count)


class SimpleNeuralNet(nn.Module):
    def __init__(self, input_dim: int):
        super().__init__()
        self.crit = nn.BCEWithLogitsLoss()
        self.fc = nn.Sequential(
            nn.Linear(input_dim, 1),
            # nn.Sigmoid(),
        )

    def forward(self, x: torch.Tensor):
        out: torch.Tensor = self.fc(x)
        return out.squeeze(dim=1)

    def fit(self, cfg: TrainConfig, trn_ldr: DataLoader):
        optm = optim.Adam(self.parameters(), lr=cfg.lr, weight_decay=cfg.weight_decay)
        self.to(cfg.device)
        super().train(True)
        for epoch in range(cfg.num_epochs):
            print(f"Epoch ( {epoch} )")
            losses: list[float] = []
            for i, (x_batch, y_batch) in enumerate(tqdm(trn_ldr)):
                optm.zero_grad()
                x_batch: torch.Tensor = x_batch.to(cfg.device)
                y_batch: torch.Tensor = y_batch.to(cfg.device)
                pred: torch.Tensor = self(x_batch)
                loss: torch.Tensor = self.crit(pred, y_batch)
                loss.backward()
                optm.step()
                losses.append(loss.item())

            print(f"Losses: {sum(losses)}")

    def accuracy(self, cfg: TrainConfig, val_ldr: DataLoader):
        correct = 0
        total = 0
        self.to(cfg.device)

        super().eval()
        with torch.no_grad():
            for i, (x_batch, y_batch) in enumerate(tqdm(val_ldr)):
                x_batch: torch.Tensor = x_batch.to(cfg.device)
                y_batch: torch.Tensor = y_batch.to(cfg.device)
                logits: torch.Tensor = self(x_batch)
                probabilities = torch.sigmoid(logits)
                preds = (probabilities >= 0.5).float()
                correct += (preds == y_batch).sum().item()
                total += y_batch.size(0)

        acc = correct / total
        print(f"Accuracy: {acc:.4f}")


if __name__ == "__main__":
    main()
