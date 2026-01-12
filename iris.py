import pandas as pd
import numpy as np
from sklearn.cluster import KMeans
import skfuzzy as fuzz
import matplotlib.pyplot as plt
from sklearn.preprocessing import StandardScaler
from sklearn.metrics import confusion_matrix, accuracy_score
from scipy.optimize import linear_sum_assignment

# 1. 读取数据
data = pd.read_csv('Iris.csv')  # 请确保文件名正确

# 2. 提取特征和真实标签
X = data.iloc[:, 0:4].values  # SepalLengthCm, SepalWidthCm, PetalLengthCm, PetalWidthCm
true_labels = data['Species'].values

# 将标签字符串转换为数字
label_mapping = {label: idx for idx, label in enumerate(np.unique(true_labels))}
true_labels_num = np.array([label_mapping[label.strip()] for label in true_labels])  # 注意去除可能的空格

# 3. 标准化特征
scaler = StandardScaler()
X_scaled = scaler.fit_transform(X)

# 4. K-Means聚类
kmeans = KMeans(n_clusters=3, random_state=42)
kmeans_labels = kmeans.fit_predict(X_scaled)

# 5. FCM聚类
# 转置数据是因为skfuzzy要求形状为 (features, samples)
cntr, u, u0, d, jm, p, fpc = fuzz.cluster.cmeans(
    X_scaled.T, c=3, m=2, error=0.005, maxiter=1000, init=None, seed=42)

# u为隶属度矩阵，选择隶属度最高的类别作为聚类结果
fcm_labels = np.argmax(u, axis=0)

# 6. 评估聚类效果（使用匈牙利算法匹配标签与聚类结果）
def cluster_acc(y_true, y_pred):
    """
    计算聚类准确率（Cluster Accuracy）
    使用匈牙利算法匹配聚类标签和真实标签。
    """
    y_true = y_true.astype(np.int64)
    assert y_pred.size == y_true.size
    D = max(y_pred.max(), y_true.max()) + 1
    w = np.zeros((D, D), dtype=np.int64)
    for i in range(y_pred.size):
        w[y_pred[i], y_true[i]] += 1
    row_ind, col_ind = linear_sum_assignment(w.max() - w)
    return sum([w[i,j] for i,j in zip(row_ind, col_ind)]) / y_pred.size

kmeans_acc = cluster_acc(true_labels_num, kmeans_labels)
fcm_acc = cluster_acc(true_labels_num, fcm_labels)

print(f"K-Means聚类准确率: {kmeans_acc:.4f}")
print(f"FCM聚类准确率: {fcm_acc:.4f}")

# 7. 简单画图展示（前两主成分）
from sklearn.decomposition import PCA

pca = PCA(n_components=2)
X_pca = pca.fit_transform(X_scaled)

plt.figure(figsize=(12,5))

plt.subplot(1, 2, 1)
plt.title("K-Means")
plt.scatter(X_pca[:,0], X_pca[:,1], c=kmeans_labels, cmap='viridis', s=50)
plt.xlabel("PC1")
plt.ylabel("PC2")

plt.subplot(1, 2, 2)
plt.title("FCM")
plt.scatter(X_pca[:,0], X_pca[:,1], c=fcm_labels, cmap='viridis', s=50)
plt.xlabel("PC1")
plt.ylabel("PC2")

plt.show()
