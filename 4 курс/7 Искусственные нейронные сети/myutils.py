import os
import random
from dataclasses import dataclass

import numpy as np
import torch


def fix_seeds(seed: int):
    random.seed(seed)
    np.random.seed(seed)
    torch.manual_seed(seed)
    torch.cuda.manual_seed_all(seed)
    if torch.backends.mps.is_available():
        torch.mps.manual_seed(seed)
    os.environ["CUBLAS_WORKSPACE_CONFIG"] = ":4096:8"
    torch.use_deterministic_algorithms(True)


@dataclass
class TrainConfig:
    seed: int = 42
    lr: float = 1e-3
    num_epochs: int = 200
    weight_decay: float = 1e-5
    batch_size: int = 256
    device: torch.device = (
        torch.device("cuda") if torch.cuda.is_available() else torch.device("cpu")
    )
