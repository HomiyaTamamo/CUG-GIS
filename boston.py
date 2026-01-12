import numpy as np
import pandas as pd
import tkinter as tk
from tkinter import scrolledtext, Canvas, Frame, Label, Button, Toplevel, ttk
import math


class BostonHousingAnalysis:
    def __init__(self):
        # 初始化主窗口
        self.root = tk.Tk()
        self.root.title("波士顿房价预测分析系统")
        self.root.geometry("1200x800")
        self.root.configure(bg="#f0f0f0")

        # 创建主框架
        self.main_frame = ttk.Frame(self.root)
        self.main_frame.pack(fill=tk.BOTH, expand=True, padx=20, pady=20)

        # 创建标签页
        self.notebook = ttk.Notebook(self.main_frame)
        self.notebook.pack(fill=tk.BOTH, expand=True)

        # 创建标签页
        self.create_welcome_tab()
        self.create_model_tab()
        self.create_feature_tab()
        self.create_visualization_tab()

        # 加载数据
        self.load_data()

        # 运行主循环
        self.root.mainloop()

    def load_data(self):
        """加载数据集"""
        try:
            # 1. 加载数据
            self.train_data = pd.read_csv('boston_house.train', delim_whitespace=True, header=None)
            self.test_data = pd.read_csv('boston_house.test', delim_whitespace=True, header=None)

            # 2. 数据预处理
            # 列名定义
            self.column_names = ['CRIM', 'ZN', 'INDUS', 'CHAS', 'NOX', 'RM', 'AGE',
                                 'DIS', 'RAD', 'TAX', 'PTRATIO', 'B', 'LSTAT', 'MEDV']

            self.train_data.columns = self.column_names
            self.test_data.columns = self.column_names

            # 分离特征和目标变量
            X_train = self.train_data.drop('MEDV', axis=1).values
            y_train = self.train_data['MEDV'].values.reshape(-1, 1)
            X_test = self.test_data.drop('MEDV', axis=1).values
            y_test = self.test_data['MEDV'].values.reshape(-1, 1)

            # 添加偏置项 (截距)
            self.X_train = np.hstack([np.ones((X_train.shape[0], 1)), X_train])
            self.X_test = np.hstack([np.ones((X_test.shape[0], 1)), X_test])
            self.y_train = y_train
            self.y_test = y_test

            # 标准化特征数据
            self.standardize_data()

            # 训练模型
            self.train_models()

            # 更新UI
            self.update_welcome_tab()
            self.update_model_tab()
            self.update_feature_tab()

            # 在可视化标签页中初始化图表
            self.plot_price_trends()
            self.plot_feature_importance()

        except Exception as e:
            error_msg = f"加载数据时出错: {str(e)}"
            self.welcome_text.configure(state='normal')
            self.welcome_text.insert(tk.END, "\n\n" + error_msg)
            self.welcome_text.configure(state='disabled')

    def standardize_data(self):
        """数据标准化 (Z-score标准化)"""
        # 标准化特征数据 (不包括偏置项)
        self.X_train[:, 1:] = self.standardize(self.X_train[:, 1:])
        self.X_test[:, 1:] = self.standardize(self.X_test[:, 1:])

    @staticmethod
    def standardize(X):
        """数据标准化函数"""
        mean = np.mean(X, axis=0)
        std = np.std(X, axis=0)
        std[std == 0] = 1  # 避免除以零
        return (X - mean) / std

    def train_models(self):
        """训练所有模型"""
        # 初始化模型
        self.models = {
            'Lasso': LassoRegression(alpha=0.1),
            'Ridge': RidgeRegression(alpha=1.0),
            'ElasticNet': ElasticNetRegression(alpha=0.1, l1_ratio=0.5)
        }

        self.results = []

        for name, model in self.models.items():
            # 训练模型
            model.fit(self.X_train, self.y_train)

            # 预测
            y_pred = model.predict(self.X_test)

            # 评估指标
            mse = mean_squared_error(self.y_test, y_pred)
            rmse = root_mean_squared_error(self.y_test, y_pred)
            r2 = r2_score(self.y_test, y_pred)

            # 存储结果
            self.results.append({
                'Model': name,
                'MSE': mse,
                'RMSE': rmse,
                'R2': r2,
                'Weights': model.weights.flatten()
            })

    def create_welcome_tab(self):
        """创建欢迎标签页"""
        self.welcome_tab = ttk.Frame(self.notebook)
        self.notebook.add(self.welcome_tab, text="欢迎")

        # 创建欢迎文本区域
        welcome_frame = ttk.LabelFrame(self.welcome_tab, text="波士顿房价分析系统")
        welcome_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        self.welcome_text = scrolledtext.ScrolledText(welcome_frame, wrap=tk.WORD, font=("Arial", 11))
        self.welcome_text.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        # 添加欢迎信息
        welcome_msg = """
        ===========================================================
                         波士顿房价预测分析系统
        ===========================================================

        本系统使用波士顿房屋数据集，实现三种回归模型进行房价预测：
          1. Lasso回归（套索回归）
          2. Ridge回归（岭回归）
          3. ElasticNet回归（弹性网回归）

        系统功能：
          - 模型比较：比较三种回归模型的性能
          - 特征分析：分析各特征对房价的影响
          - 可视化：展示房价变化趋势和特征重要性

        数据集说明：
          数据集包含506个样本，14个特征，包括：
            CRIM: 城镇人均犯罪率
            ZN: 住宅用地所占比例
            INDUS: 城镇中非住宅用地所占比例
            CHAS: 查尔斯河位置（虚拟变量）
            NOX: 环保指数
            RM: 每栋住宅的房间数
            AGE: 1940年以前建成的自住单位比例
            DIS: 距离5个波士顿就业中心的加权距离
            RAD: 距离高速公路的便利指数
            TAX: 每万美元不动产税率
            PTRATIO: 城镇中的教师学生比例
            B: 城镇中的黑人比例
            LSTAT: 地区中低收入房东比例
            MEDV: 自住房屋房价中位数

        请切换到其他标签页查看分析结果...
        """
        self.welcome_text.insert(tk.INSERT, welcome_msg)
        self.welcome_text.configure(state='disabled')

    def update_welcome_tab(self):
        """更新欢迎标签页信息"""
        self.welcome_text.configure(state='normal')
        self.welcome_text.insert(tk.END, "\n\n数据加载完成！")
        self.welcome_text.insert(tk.END, f"\n训练集样本数: {len(self.train_data)}")
        self.welcome_text.insert(tk.END, f"\n测试集样本数: {len(self.test_data)}")
        self.welcome_text.configure(state='disabled')

    def create_model_tab(self):
        """创建模型比较标签页"""
        self.model_tab = ttk.Frame(self.notebook)
        self.notebook.add(self.model_tab, text="模型比较")

        # 创建模型比较框架
        model_frame = ttk.LabelFrame(self.model_tab, text="回归模型性能比较")
        model_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        # 创建文本框显示结果
        self.model_text = scrolledtext.ScrolledText(model_frame, wrap=tk.WORD, font=("Arial", 10))
        self.model_text.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

    def update_model_tab(self):
        """更新模型比较标签页"""
        self.model_text.configure(state='normal')
        self.model_text.delete(1.0, tk.END)

        # 添加标题
        title = "回归模型性能比较\n" + "=" * 80 + "\n\n"
        self.model_text.insert(tk.INSERT, title)

        # 显示每个模型的结果
        for res in self.results:
            self.model_text.insert(tk.INSERT, f"{res['Model']} 回归结果:\n")
            self.model_text.insert(tk.INSERT, f"  MSE (均方误差): {res['MSE']:.4f}\n")
            self.model_text.insert(tk.INSERT, f"  RMSE (均方根误差): {res['RMSE']:.4f}\n")
            self.model_text.insert(tk.INSERT, f"  R² (决定系数): {res['R2']:.4f}\n")

            # 显示系数
            self.model_text.insert(tk.INSERT, "\n  模型权重:\n")
            self.model_text.insert(tk.INSERT, f"    截距项: {res['Weights'][0]:.6f}\n")
            for i in range(1, len(res['Weights'])):
                self.model_text.insert(tk.INSERT, f"    {self.column_names[i - 1]}: {res['Weights'][i]:.6f}\n")

            self.model_text.insert(tk.INSERT, "-" * 80 + "\n\n")

        # 性能比较
        best_r2 = max(self.results, key=lambda x: x['R2'])
        best_rmse = min(self.results, key=lambda x: x['RMSE'])

        self.model_text.insert(tk.INSERT, "\n模型性能总结:\n")
        self.model_text.insert(tk.INSERT, "=" * 80 + "\n")
        self.model_text.insert(tk.INSERT, f"最佳R²分数: {best_r2['R2']:.4f} ({best_r2['Model']})\n")
        self.model_text.insert(tk.INSERT, f"最低RMSE: {best_rmse['RMSE']:.4f} ({best_rmse['Model']})\n")

        # 分析报告
        self.model_text.insert(tk.INSERT, "\n分析报告:\n")
        self.model_text.insert(tk.INSERT, "=" * 80 + "\n")
        self.model_text.insert(tk.INSERT, "1. 模型实现说明:\n")
        self.model_text.insert(tk.INSERT, "   - 普通最小二乘回归: 使用正规方程直接求解\n")
        self.model_text.insert(tk.INSERT, "   - 岭回归: 添加L2正则化项，使用正规方程求解\n")
        self.model_text.insert(tk.INSERT, "   - 套索回归: 使用坐标下降法实现L1正则化\n")
        self.model_text.insert(tk.INSERT, "   - 弹性网回归: 结合L1和L2正则化，使用坐标下降法\n\n")

        self.model_text.insert(tk.INSERT, "2. 模型特点比较:\n")
        self.model_text.insert(tk.INSERT, "   - Lasso回归 (套索): 产生稀疏权重，自动执行特征选择\n")
        self.model_text.insert(tk.INSERT, "   - Ridge回归 (岭): 缩小所有权重，处理多重共线性\n")
        self.model_text.insert(tk.INSERT, "   - ElasticNet (弹性网): 平衡L1和L2正则化，结合两者优点\n\n")

        # 系数稀疏性分析
        lasso_zero = sum(abs(w) < 0.001 for w in self.results[0]['Weights'][1:])
        en_zero = sum(abs(w) < 0.001 for w in self.results[2]['Weights'][1:])

        self.model_text.insert(tk.INSERT, "3. 系数稀疏性分析:\n")
        self.model_text.insert(tk.INSERT, f"   Lasso回归中接近零的权重数: {lasso_zero}\n")
        self.model_text.insert(tk.INSERT, f"   ElasticNet回归中接近零的权重数: {en_zero}\n")
        self.model_text.insert(tk.INSERT, f"   Ridge回归中接近零的权重数: 0 (所有权重都被保留)\n")

        # 模型推荐
        self.model_text.insert(tk.INSERT, "\n4. 模型推荐:\n")
        self.model_text.insert(tk.INSERT, f"   基于当前数据集，{best_r2['Model']}回归表现最佳，推荐使用\n")
        self.model_text.insert(tk.INSERT, f"   若需要特征选择，Lasso回归可能是更好的选择\n")

        self.model_text.configure(state='disabled')

    def create_feature_tab(self):
        """创建特征分析标签页"""
        self.feature_tab = ttk.Frame(self.notebook)
        self.notebook.add(self.feature_tab, text="特征分析")

        # 创建特征分析框架
        feature_frame = ttk.LabelFrame(self.feature_tab, text="特征对房价的影响分析")
        feature_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        # 创建文本框显示结果
        self.feature_text = scrolledtext.ScrolledText(feature_frame, wrap=tk.WORD, font=("Arial", 10))
        self.feature_text.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

    def update_feature_tab(self):
        """更新特征分析标签页"""
        self.feature_text.configure(state='normal')
        self.feature_text.delete(1.0, tk.END)

        # 添加标题
        title = "特征对房价的影响分析\n" + "=" * 80 + "\n\n"
        self.feature_text.insert(tk.INSERT, title)

        # 使用线性回归模型获取特征权重
        lr_model = LinearRegression()
        lr_model.fit(self.X_train, self.y_train)
        lr_weights = lr_model.weights.flatten()
        feature_weights = lr_weights[1:]

        # 获取最重要的特征（按绝对值排序）
        important_features = sorted(
            [(self.column_names[i], abs(weight), weight) for i, weight in enumerate(feature_weights)],
            key=lambda x: x[1], reverse=True
        )

        self.feature_text.insert(tk.INSERT, "特征对房价影响分析报告:\n")
        self.feature_text.insert(tk.INSERT, "=" * 80 + "\n\n")

        self.feature_text.insert(tk.INSERT, "1. 对房价有重大影响的特征:\n")
        for feature, importance, weight in important_features[:3]:
            effect = "正相关" if weight > 0 else "负相关"
            self.feature_text.insert(tk.INSERT, f"   - {feature}: 影响强度 {importance:.4f} ({effect})\n")
            if feature == 'LSTAT':
                self.feature_text.insert(tk.INSERT, "     (地区中低收入人群比例越高，房价越低)\n")
            elif feature == 'RM':
                self.feature_text.insert(tk.INSERT, "     (房间数量越多，房价越高)\n")
            elif feature == 'DIS':
                self.feature_text.insert(tk.INSERT, "     (距离就业中心越近，房价越高)\n")

        self.feature_text.insert(tk.INSERT, "\n2. 对房价有中等影响的特征:\n")
        for feature, importance, weight in important_features[3:6]:
            effect = "正相关" if weight > 0 else "负相关"
            self.feature_text.insert(tk.INSERT, f"   - {feature}: 影响强度 {importance:.4f} ({effect})\n")

        self.feature_text.insert(tk.INSERT, "\n3. 对房价影响较小的特征:\n")
        for feature, importance, weight in important_features[9:]:
            effect = "正相关" if weight > 0 else "负相关"
            self.feature_text.insert(tk.INSERT, f"   - {feature}: 影响强度 {importance:.4f} ({effect})\n")

        self.feature_text.insert(tk.INSERT, "\n4. 特殊特征分析:\n")
        self.feature_text.insert(tk.INSERT, "   - CHAS: 查尔斯河虚拟变量，靠近河岸(1)的房屋价格较高\n")
        self.feature_text.insert(tk.INSERT, "   - ZN: 住宅用地比例，对房价有中等影响\n")
        self.feature_text.insert(tk.INSERT, "   - INDUS: 非住宅用地比例，与房价呈负相关\n")
        self.feature_text.insert(tk.INSERT, "   - RAD: 高速公路便利指数，与房价呈正相关\n")
        self.feature_text.insert(tk.INSERT, "   - TAX: 不动产税率，与房价呈负相关\n")
        self.feature_text.insert(tk.INSERT, "   - B: 黑人比例，与房价呈正相关\n")

        self.feature_text.configure(state='disabled')

    def create_visualization_tab(self):
        """创建可视化标签页"""
        self.visualization_tab = ttk.Frame(self.notebook)
        self.notebook.add(self.visualization_tab, text="可视化")

        # 创建房价趋势框架
        price_frame = ttk.LabelFrame(self.visualization_tab, text="房价变化趋势")
        price_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        # 训练集房价趋势
        train_label = ttk.Label(price_frame, text="训练集房价变化趋势", font=("Arial", 10, "bold"))
        train_label.pack(pady=5)

        self.train_canvas = Canvas(price_frame, bg="white", height=250)
        self.train_canvas.pack(fill=tk.BOTH, expand=True, padx=20, pady=5)

        # 测试集房价趋势
        test_label = ttk.Label(price_frame, text="测试集房价变化趋势", font=("Arial", 10, "bold"))
        test_label.pack(pady=5)

        self.test_canvas = Canvas(price_frame, bg="white", height=250)
        self.test_canvas.pack(fill=tk.BOTH, expand=True, padx=20, pady=5)

        # 创建特征重要性框架
        feature_frame = ttk.LabelFrame(self.visualization_tab, text="特征重要性分析")
        feature_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

        # 特征重要性图
        feature_label = ttk.Label(feature_frame, text="特征对房价的影响程度", font=("Arial", 10, "bold"))
        feature_label.pack(pady=5)

        self.feature_canvas = Canvas(feature_frame, bg="white", height=300)
        self.feature_canvas.pack(fill=tk.BOTH, expand=True, padx=20, pady=10)

    def plot_price_trends(self):
        """绘制房价趋势图"""
        # 绘制训练集房价趋势
        train_prices = self.y_train.flatten().tolist()
        self.train_canvas.bind("<Configure>", lambda e: self.plot_price_trend(
            self.train_canvas, train_prices, "训练集房价变化趋势", "blue"
        ))

        # 绘制测试集房价趋势
        test_prices = self.y_test.flatten().tolist()
        self.test_canvas.bind("<Configure>", lambda e: self.plot_price_trend(
            self.test_canvas, test_prices, "测试集房价变化趋势", "green"
        ))

    def plot_feature_importance(self):
        """绘制特征重要性图"""
        # 使用线性回归模型获取特征权重
        lr_model = LinearRegression()
        lr_model.fit(self.X_train, self.y_train)
        lr_weights = lr_model.weights.flatten()

        self.feature_canvas.bind("<Configure>", lambda e: self.plot_feature_importance_chart(
            self.feature_canvas, self.column_names[:-1], lr_weights, "特征对房价的影响"
        ))

    def plot_price_trend(self, canvas, prices, title, color="blue"):
        """在Canvas上绘制房价趋势图"""
        canvas.delete("all")
        width = canvas.winfo_width()
        height = canvas.winfo_height()
        padding = 50

        if len(prices) == 0:
            return

        # 计算缩放比例
        max_price = max(prices)
        min_price = min(prices)
        price_range = max_price - min_price
        if price_range == 0:
            price_range = 1

        # 绘制坐标轴
        canvas.create_line(padding, height - padding, width - padding, height - padding, width=2)  # X轴
        canvas.create_line(padding, padding, padding, height - padding, width=2)  # Y轴

        # 绘制刻度和标签
        for i in range(0, 11):
            # Y轴刻度
            y = padding + i * (height - 2 * padding) / 10
            value = min_price + (10 - i) * price_range / 10
            canvas.create_line(padding - 5, y, padding, y, width=1)
            canvas.create_text(padding - 10, y, text=f"{value:.1f}", anchor="e", font=("Arial", 8))

            # X轴刻度
            if i < 10:
                x = padding + i * (width - 2 * padding) / 10
                canvas.create_line(x, height - padding, x, height - padding + 5, width=1)
                canvas.create_text(x, height - padding + 20, text=f"{i * 10}%", anchor="n", font=("Arial", 8))

        # 绘制标题
        canvas.create_text(width // 2, 20, text=title, font=("Arial", 12, "bold"))

        # 绘制数据点
        points = []
        for i, price in enumerate(prices):
            x = padding + i * (width - 2 * padding) / (len(prices) - 1)
            y = padding + (max_price - price) * (height - 2 * padding) / price_range
            points.append((x, y))

        # 绘制折线
        for i in range(1, len(points)):
            x1, y1 = points[i - 1]
            x2, y2 = points[i]
            canvas.create_line(x1, y1, x2, y2, fill=color, width=2)

        # 绘制数据点
        for x, y in points:
            canvas.create_oval(x - 3, y - 3, x + 3, y + 3, fill=color, outline=color)

        # 添加说明
        canvas.create_text(width // 2, height - 10, text="样本索引百分比", anchor="n", font=("Arial", 9))

    def plot_feature_importance_chart(self, canvas, feature_names, weights, title):
        """在Canvas上绘制特征重要性图"""
        canvas.delete("all")
        width = canvas.winfo_width()
        height = canvas.winfo_height()
        padding = 50
        bar_width = 30
        spacing = 10

        # 只取特征权重（不包括截距）
        feature_weights = weights[1:]
        n_features = len(feature_weights)

        if n_features == 0:
            return

        # 计算重要性（绝对权重值）
        importances = [abs(w) for w in feature_weights]
        max_importance = max(importances) if max(importances) > 0 else 1

        # 绘制坐标轴
        canvas.create_line(padding, height - padding, width - padding, height - padding, width=2)  # X轴
        canvas.create_line(padding, padding, padding, height - padding, width=2)  # Y轴

        # 绘制标题
        canvas.create_text(width // 2, 20, text=title, font=("Arial", 12, "bold"))

        # 绘制特征重要性柱状图
        for i, (name, imp) in enumerate(zip(feature_names, importances)):
            # 计算柱状图位置
            x = padding + i * (bar_width + spacing) + bar_width // 2
            bar_height = (imp / max_importance) * (height - 2 * padding - 20)

            # 绘制柱状图
            canvas.create_rectangle(
                x - bar_width // 2, height - padding,
                x + bar_width // 2, height - padding - bar_height,
                fill="skyblue", outline="black"
            )

            # 绘制特征名称
            canvas.create_text(x, height - padding + 15, text=name, anchor="n", angle=45, font=("Arial", 8))

            # 绘制权重值
            canvas.create_text(x, height - padding - bar_height - 10, text=f"{imp:.3f}", anchor="s", font=("Arial", 8))

        # 绘制Y轴标签
        canvas.create_text(padding // 2, height // 2, text="特征重要性", angle=90, anchor="center", font=("Arial", 10))

        # 添加说明
        canvas.create_text(width // 2, height - 10, text="特征名称", anchor="n", font=("Arial", 9))


# ====================== 自定义评估指标函数 ======================
def mean_squared_error(y_true, y_pred):
    """计算均方误差 (MSE)"""
    return np.mean((y_true - y_pred) ** 2)


def root_mean_squared_error(y_true, y_pred):
    """计算均方根误差 (RMSE)"""
    return np.sqrt(mean_squared_error(y_true, y_pred))


def r2_score(y_true, y_pred):
    """计算决定系数 (R²)"""
    ss_total = np.sum((y_true - np.mean(y_true)) ** 2)
    ss_residual = np.sum((y_true - y_pred) ** 2)
    return 1 - (ss_residual / ss_total)


# ====================== 自定义回归模型 ======================
class LinearRegression:
    """普通最小二乘线性回归"""

    def fit(self, X, y):
        # 使用正规方程计算权重
        self.weights = np.linalg.inv(X.T @ X) @ X.T @ y
        return self

    def predict(self, X):
        return X @ self.weights


class RidgeRegression:
    """岭回归 (L2正则化)"""

    def __init__(self, alpha=1.0):
        self.alpha = alpha  # 正则化强度

    def fit(self, X, y):
        # 添加正则化项
        I = np.eye(X.shape[1])
        I[0, 0] = 0  # 不对偏置项进行正则化
        self.weights = np.linalg.inv(X.T @ X + self.alpha * I) @ X.T @ y
        return self

    def predict(self, X):
        return X @ self.weights


class LassoRegression:
    """套索回归 (L1正则化) - 使用坐标下降法"""

    def __init__(self, alpha=0.1, max_iter=1000, tol=1e-4):
        self.alpha = alpha  # 正则化强度
        self.max_iter = max_iter  # 最大迭代次数
        self.tol = tol  # 收敛容忍度

    def fit(self, X, y):
        n_samples, n_features = X.shape
        self.weights = np.zeros((n_features, 1))  # 初始化权重

        # 坐标下降算法
        for _ in range(self.max_iter):
            max_change = 0
            for j in range(n_features):
                # 计算残差 (不包括当前特征)
                r = y - X @ self.weights + X[:, j:j + 1] * self.weights[j]

                # 计算最小二乘解
                xj = X[:, j:j + 1]
                rho_j = xj.T @ r

                # 应用软阈值函数 (L1正则化)
                if j == 0:  # 不对偏置项进行正则化
                    self.weights[j] = rho_j / (xj.T @ xj)
                else:
                    zj = xj.T @ xj
                    self.weights[j] = self.soft_threshold(rho_j, self.alpha) / zj

                # 更新最大变化量
                max_change = max(max_change, abs(rho_j[0, 0]))

            # 检查收敛
            if max_change < self.tol:
                break

        return self

    @staticmethod
    def soft_threshold(rho, alpha):
        """软阈值函数"""
        if rho < -alpha:
            return rho + alpha
        elif rho > alpha:
            return rho - alpha
        else:
            return 0

    def predict(self, X):
        return X @ self.weights


class ElasticNetRegression:
    """弹性网回归 (L1 + L2正则化) - 使用坐标下降法"""

    def __init__(self, alpha=0.1, l1_ratio=0.5, max_iter=1000, tol=1e-4):
        self.alpha = alpha  # 正则化强度
        self.l1_ratio = l1_ratio  # L1正则化比例
        self.max_iter = max_iter  # 最大迭代次数
        self.tol = tol  # 收敛容忍度

    def fit(self, X, y):
        n_samples, n_features = X.shape
        self.weights = np.zeros((n_features, 1))  # 初始化权重

        # 坐标下降算法
        for _ in range(self.max_iter):
            max_change = 0
            for j in range(n_features):
                # 计算残差 (不包括当前特征)
                r = y - X @ self.weights + X[:, j:j + 1] * self.weights[j]

                # 计算最小二乘解
                xj = X[:, j:j + 1]
                rho_j = xj.T @ r

                # 应用弹性网正则化
                if j == 0:  # 不对偏置项进行正则化
                    self.weights[j] = rho_j / (xj.T @ xj)
                else:
                    zj = xj.T @ xj
                    l1_penalty = self.alpha * self.l1_ratio
                    l2_penalty = self.alpha * (1 - self.l1_ratio)

                    # 应用软阈值函数 (L1正则化)
                    soft_val = self.soft_threshold(rho_j, l1_penalty)

                    # 应用L2正则化
                    self.weights[j] = soft_val / (zj + l2_penalty)

                # 更新最大变化量
                max_change = max(max_change, abs(rho_j[0, 0]))

            # 检查收敛
            if max_change < self.tol:
                break

        return self

    @staticmethod
    def soft_threshold(rho, alpha):
        """软阈值函数"""
        if rho < -alpha:
            return rho + alpha
        elif rho > alpha:
            return rho - alpha
        else:
            return 0

    def predict(self, X):
        return X @ self.weights


# ====================== 启动应用 ======================
if __name__ == "__main__":
    app = BostonHousingAnalysis()