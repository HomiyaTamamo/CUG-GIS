#忽略编译器报错，这东西完全能用
import tarfile
import os
import shutil
from sklearn.datasets import load_files
from sklearn.model_selection import train_test_split
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import classification_report, accuracy_score

# ===== 解压数据集 =====
def extract_dataset(archive_path, extract_to='20news_data'):
    if os.path.exists(extract_to):
        shutil.rmtree(extract_to)
    with tarfile.open(archive_path, 'r:gz') as tar:
        tar.extractall(path=extract_to)
    print(f"✅ 数据集已解压到：{extract_to}")

# ===== 加载数据集 =====
def load_dataset(data_dir):
    data = load_files(data_dir, encoding='latin1', decode_error='ignore')
    return data.data, data.target, data.target_names

# ===== 主分类函数 =====
def classify_20news():
    archive_path = '20news-19997.tar.gz'  # 确保该文件已下载到当前目录
    extract_dir = '20news_data'

    extract_dataset(archive_path, extract_to=extract_dir)

    # 加载数据
    data_path = os.path.join(extract_dir, '20_newsgroups')
    texts, labels, label_names = load_dataset(data_path)
    print(f"✅ 共加载 {len(texts)} 条文本，类别数：{len(label_names)}")

    # 划分训练/测试集
    X_train, X_test, y_train, y_test = train_test_split(
        texts, labels, test_size=0.2, random_state=42
    )

    # TF-IDF 向量化
    vectorizer = TfidfVectorizer(max_features=10000, stop_words='english')
    X_train_vec = vectorizer.fit_transform(X_train)
    X_test_vec = vectorizer.transform(X_test)

    # 逻辑回归训练
    clf = LogisticRegression(max_iter=1000)
    clf.fit(X_train_vec, y_train)

    # 预测与评估
    y_pred = clf.predict(X_test_vec)
    accuracy = accuracy_score(y_test, y_pred)

    print("\n===== 分类报告 =====")
    print(classification_report(y_test, y_pred, target_names=label_names))

    print(f"\n✅ 测试集准确率：{accuracy:.4f}")

if __name__ == '__main__':
    classify_20news()
