import numpy as np
import scipy.io as sio
from sklearn.cluster import KMeans
from sklearn.metrics import (
    adjusted_rand_score,
    normalized_mutual_info_score,
    fowlkes_mallows_score
)
import matplotlib.pyplot as plt


# --------- Step 1: Load hyperspectral data ---------
def load_data():
    data = sio.loadmat('PaviaU.mat')['paviaU']  # (610, 340, 103)
    gt = sio.loadmat('PaviaU_gt.mat')['paviaU_gt']  # (610, 340)
    return data, gt


# --------- Step 2: Preprocess: reshape and mask ---------
def preprocess(data, gt):
    h, w, bands = data.shape
    data_2d = data.reshape(-1, bands)
    gt_flat = gt.flatten()
    mask = gt_flat > 0
    X = data_2d[mask]
    y_true = gt_flat[mask]
    return X, y_true, mask, h, w


# --------- Step 3: Run K-means clustering ---------
def run_kmeans(X, n_clusters):
    kmeans = KMeans(n_clusters=n_clusters, random_state=42, n_init='auto')
    y_pred = kmeans.fit_predict(X)
    return y_pred


# --------- Step 4: Visualize multiple evaluation metrics ---------
def visualize(y_preds, mask, h, w, scores):
    fig, axs = plt.subplots(1, 3, figsize=(18, 6))
    titles = [
        f'ARI = {scores["ARI"]:.4f}',
        f'NMI = {scores["NMI"]:.4f}',
        f'FMI = {scores["FMI"]:.4f}'
    ]

    for ax, (key, y_pred), title in zip(axs, y_preds.items(), titles):
        result = np.zeros(h * w)
        result[mask] = y_pred + 1
        result_img = result.reshape(h, w)
        ax.imshow(result_img, cmap='nipy_spectral')
        ax.set_title(title, fontsize=14)
        ax.axis('off')

    plt.suptitle("K-means Clustering with Different Evaluation Metrics", fontsize=16)
    plt.tight_layout(rect=[0, 0, 1, 0.95])
    plt.show()


# --------- Step 5: Main workflow ---------
def main():
    print("Loading PaviaU data...")
    data, gt = load_data()
    X, y_true, mask, h, w = preprocess(data, gt)
    n_clusters = len(np.unique(y_true))

    print(f"Running K-means clustering (k={n_clusters})...")
    y_pred = run_kmeans(X, n_clusters)

    # Evaluation
    scores = {
        "ARI": adjusted_rand_score(y_true, y_pred),
        "NMI": normalized_mutual_info_score(y_true, y_pred),
        "FMI": fowlkes_mallows_score(y_true, y_pred)
    }

    print("Evaluation Results:")
    for k, v in scores.items():
        print(f"{k}: {v:.4f}")

    y_preds = {
        "ARI": y_pred,
        "NMI": y_pred,
        "FMI": y_pred
    }

    visualize(y_preds, mask, h, w, scores)


if __name__ == "__main__":
    main()
