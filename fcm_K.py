import numpy as np
import scipy.io as sio
import matplotlib.pyplot as plt
from sklearn.cluster import KMeans
from sklearn.metrics import adjusted_rand_score
import skfuzzy as fuzz


# --------- Step 1: Load and preprocess PaviaU data ---------
def load_data():
    data = sio.loadmat('PaviaU.mat')['paviaU']
    labels = sio.loadmat('PaviaU_gt.mat')['paviaU_gt']
    return data, labels


def preprocess(data, labels):
    h, w, bands = data.shape
    data_2d = data.reshape(-1, bands)
    labels_flat = labels.flatten()
    mask = labels_flat > 0
    X = data_2d[mask]
    y_true = labels_flat[mask]
    return X, y_true, mask, h, w


# --------- Step 2: KMeans Clustering ---------
def run_kmeans(X, n_clusters):
    kmeans = KMeans(n_clusters=n_clusters, random_state=0, n_init='auto')
    y_pred = kmeans.fit_predict(X)
    return y_pred


# --------- Step 3: FCM Clustering ---------
def run_fcm(X, n_clusters):
    X_T = X.T  # FCM expects features as rows
    cntr, u, _, _, _, _, _ = fuzz.cluster.cmeans(X_T, c=n_clusters, m=2, error=0.005, maxiter=1000)
    y_pred = np.argmax(u, axis=0)
    return y_pred


# --------- Step 4: Visualization ---------
def visualize_comparison(kmeans_pred, fcm_pred, mask, h, w):
    img_kmeans = np.zeros(h * w)
    img_fcm = np.zeros(h * w)
    img_kmeans[mask] = kmeans_pred + 1
    img_fcm[mask] = fcm_pred + 1
    img_kmeans = img_kmeans.reshape(h, w)
    img_fcm = img_fcm.reshape(h, w)

    plt.figure(figsize=(12, 5))

    plt.subplot(1, 2, 1)
    plt.imshow(img_kmeans, cmap='nipy_spectral')
    plt.title('K-Means Clustering')
    plt.axis('off')

    plt.subplot(1, 2, 2)
    plt.imshow(img_fcm, cmap='nipy_spectral')
    plt.title('FCM Clustering')
    plt.axis('off')

    plt.suptitle('K-Means vs FCM Clustering on PaviaU Data', fontsize=15)
    plt.tight_layout(rect=[0, 0, 1, 0.95])
    plt.show()


# --------- Step 5: Main ---------
def main():
    print("🔍 Loading data...")
    data, labels = load_data()
    X, y_true, mask, h, w = preprocess(data, labels)
    n_clusters = len(np.unique(y_true))

    print("🔢 Running K-Means...")
    kmeans_pred = run_kmeans(X, n_clusters)
    ari_kmeans = adjusted_rand_score(y_true, kmeans_pred)
    print(f"K-Means ARI: {ari_kmeans:.4f}")

    print("🌀 Running FCM...")
    fcm_pred = run_fcm(X, n_clusters)
    ari_fcm = adjusted_rand_score(y_true, fcm_pred)
    print(f"FCM ARI: {ari_fcm:.4f}")

    visualize_comparison(kmeans_pred, fcm_pred, mask, h, w)


if __name__ == '__main__':
    main()
