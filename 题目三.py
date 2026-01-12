import tkinter as tk
from tkinter import filedialog, ttk, messagebox
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.naive_bayes import GaussianNB
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import accuracy_score, classification_report

# ---------- 数据加载 ----------
def load_data(filenames):
    data_list = []
    for fname in filenames:
        with open(fname, 'r') as f:
            for line_num, line in enumerate(f, start=1):
                line = line.strip()
                if not line or line.startswith('#'):
                    continue  # 跳过空行和注释
                parts = line.split(',')
                if len(parts) != 5:
                    continue  # 跳过格式错误行
                try:
                    features = list(map(float, parts[:-1]))
                    label = int(parts[-1])
                    data_list.append(features + [label])
                except ValueError:
                    continue
    if not data_list:
        raise ValueError("❌ 没有读取到任何有效样本，请检查文件。")
    data_array = np.array(data_list)
    X = data_array[:, :-1]
    y = data_array[:, -1].astype(int)
    return X, y

# ---------- GUI 应用类 ----------
class IrisClassifierApp:
    def __init__(self, root):
        self.root = root
        self.root.title("鸢尾花分类器")
        self.root.geometry("700x500")
        self.files = ["mod-iris.txt", "iris-data.txt"]

        try:
            self.X, self.y = load_data(self.files)
        except Exception as e:
            messagebox.showerror("数据加载错误", str(e))
            self.root.destroy()
            return

        self.create_widgets()

    def create_widgets(self):
        # 分类器选择
        self.model_var = tk.StringVar(value="naive_bayes")
        ttk.Label(self.root, text="选择分类算法:").pack(pady=5)
        ttk.Radiobutton(self.root, text="朴素贝叶斯", variable=self.model_var, value="naive_bayes").pack()
        ttk.Radiobutton(self.root, text="决策树", variable=self.model_var, value="decision_tree").pack()

        # 运行按钮
        ttk.Button(self.root, text="开始分类", command=self.run_classification).pack(pady=10)

        # 结果文本框
        self.result_text = tk.Text(self.root, height=20, width=80)
        self.result_text.pack(padx=10, pady=10)

    def run_classification(self):
        # 划分训练集/测试集
        X_train, X_test, y_train, y_test = train_test_split(self.X, self.y, test_size=0.3, random_state=0)

        # 分类器选择
        if self.model_var.get() == "naive_bayes":
            clf = GaussianNB()
        else:
            clf = DecisionTreeClassifier()

        clf.fit(X_train, y_train)
        y_pred = clf.predict(X_test)
        acc = accuracy_score(y_test, y_pred)
        report = classification_report(y_test, y_pred, digits=3)

        # 显示结果
        self.result_text.delete(1.0, tk.END)
        self.result_text.insert(tk.END, f"分类器: {'朴素贝叶斯' if self.model_var.get() == 'naive_bayes' else '决策树'}\n")
        self.result_text.insert(tk.END, f"\n准确率: {acc:.4f}\n\n")
        self.result_text.insert(tk.END, report)

# ---------- 主程序 ----------
if __name__ == "__main__":
    root = tk.Tk()
    app = IrisClassifierApp(root)
    root.mainloop()
