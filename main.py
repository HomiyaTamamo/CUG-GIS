###001-时间序列可视化#########

import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
import pandas as pd
import networkx as nx
from pyecharts.charts import Sunburst, Pie
from pyecharts import options as opts

 # 示例数据
x = np.linspace(0, 10, 100)
y1 = np.sin(x)
y2 = np.cos(x)
categories = ['A', 'B', 'C', 'D']
values = [15, 30, 45, 10]
data = np.random.rand(10, 4)

# 设置全局字体为支持中文的字体
plt.rcParams['font.sans-serif'] = ['SimHei']  # 使用黑体
plt.rcParams['axes.unicode_minus'] = False  # 解决负号显示问题


# 1. 折线图
plt.figure(figsize=(8, 4))
plt.plot(x, y1, label='sin(x)', color='blue')
plt.plot(x, y2, label='cos(x)', color='red')
plt.title('折线图示例')
plt.xlabel('X轴')
plt.ylabel('Y轴')
plt.legend()
plt.grid()
plt.show()

# 2. 散点图
plt.figure(figsize=(8, 4))
plt.scatter(x, y1, label='sin(x)', color='green')
plt.scatter(x, y2, label='cos(x)', color='purple')
plt.title('散点图示例')
plt.xlabel('X轴')
plt.ylabel('Y轴')
plt.legend()
plt.grid()
plt.show()

# 3. 柱状图（条形图）
plt.figure(figsize=(8, 4))
plt.bar(categories, values, color=['skyblue', 'salmon', 'lightgreen', 'gold'])
plt.title('柱状图示例')
plt.xlabel('类别')
plt.ylabel('值')
plt.show()

# 4. 面积图
plt.figure(figsize=(8, 4))
plt.fill_between(x, y1, color='blue', alpha=0.3, label='sin(x) 面积')
plt.fill_between(x, y2, color='red', alpha=0.3, label='cos(x) 面积')
plt.title('面积图示例')
plt.xlabel('X轴')
plt.ylabel('Y轴')
plt.legend()
plt.grid()
plt.show()

# 5. 箱线图
plt.figure(figsize=(8, 4))
plt.boxplot([np.random.normal(0, std, 100) for std in range(1, 4)])
plt.title('箱线图示例')
plt.xlabel('组别')
plt.ylabel('值')
plt.xticks([1, 2, 3], ['组1', '组2', '组3'])
plt.show()

# 6. 饼图
plt.figure(figsize=(8, 6))
plt.pie(values, labels=categories, autopct='%1.1f%%', colors=['skyblue', 'salmon', 'lightgreen', 'gold'])
plt.title('饼图示例')
plt.show()

# 7. 旭日图（使用 pyecharts）
sunburst = (
    Sunburst()
    .add(
        series_name="旭日图示例",
        data_pair=[
            ("A", [("A1", 10), ("A2", 20)]),
            ("B", [("B1", 15), ("B2", 25)]),
            ("C", [("C1", 30), ("C2", 15)]),
        ],
        radius=[0, "90%"],
    )
    .set_global_opts(title_opts=opts.TitleOpts(title="旭日图示例"))
)
sunburst.render("sunburst.html")  # 输出到HTML文件

# 8. 热图（色块图）
plt.figure(figsize=(8, 6))
sns.heatmap(data, annot=True, fmt=".2f", cmap="YlGnBu")  # 使用 fmt=".2f" 格式化浮点数
plt.title('热图示例')
plt.xlabel('X轴')
plt.ylabel('Y轴')
plt.show()

# 9. 雷达图（蜘蛛网图）
theta = np.linspace(0, 2 * np.pi, len(categories), endpoint=False).tolist()
theta += theta[:1]  # 闭合图形
values += values[:1]

plt.figure(figsize=(8, 6))
ax = plt.subplot(111, projection='polar')
ax.plot(theta, values, 'o-', linewidth=2)
ax.fill(theta, values, alpha=0.25)
ax.set_thetagrids(np.degrees(theta[:-1]), categories)
ax.set_title('雷达图示例', va='bottom')
plt.show()

# 10. 玫瑰图（使用 pyecharts）
pie = (
    Pie()
    .add(
        series_name="玫瑰图示例",
        data_pair=list(zip(categories, values)),
        radius=["30%", "75%"],
        rosetype="radius",
    )
    .set_global_opts(title_opts=opts.TitleOpts(title="玫瑰图示例"))
)
pie.render("rose_chart.html")  # 输出到HTML文件

# 11. 直方图
plt.figure(figsize=(8, 4))
plt.hist(np.random.normal(0, 1, 1000), bins=30, color='skyblue', edgecolor='black')
plt.title('直方图示例')
plt.xlabel('值')
plt.ylabel('频数')
plt.grid(axis='y', linestyle='--', alpha=0.7)
plt.show()

# 12. 甘特图（使用 matplotlib 简单模拟）
fig, ax = plt.subplots(figsize=(10, 4))
tasks = ['任务1', '任务2', '任务3']
starts = [1, 3, 5]
durations = [2, 3, 2]
colors = ['skyblue', 'salmon', 'lightgreen']

for task, start, duration, color in zip(tasks, starts, durations, colors):
    ax.broken_barh([(start, duration)], (tasks.index(task), 0.8), facecolors=color)

ax.set_xlabel('时间')
ax.set_yticks([i + 0.4 for i in range(len(tasks))])
ax.set_yticklabels(tasks)
ax.set_title('甘特图示例')
plt.grid(True, axis='x', linestyle='--', alpha=0.7)
plt.show()

# 13. 点阵图（与散点图类似，此处简化）
plt.figure(figsize=(8, 4))
plt.scatter(np.random.rand(50), np.random.rand(50), color='orange')
plt.title('点阵图示例')
plt.xlabel('X轴')
plt.ylabel('Y轴')
plt.grid()
plt.show()

# 14. 平行坐标图（使用 pandas）
df = pd.DataFrame(np.random.rand(10, 4), columns=['特征1', '特征2', '特征3', '特征4'])
pd.plotting.parallel_coordinates(df, '特征1', color=['#556270', '#EE6666', '#73C6B6', '#4ECDC4'])  # 修正颜色代码
plt.title('平行坐标图示例')
plt.grid()
plt.show()

# 15. 网络图（使用 networkx）
G = nx.Graph()
G.add_edges_from([(1, 2), (1, 3), (2, 3), (3, 4)])
pos = nx.spring_layout(G)
nx.draw(G, pos, with_labels=True, node_color='skyblue', node_size=700, edge_color='k', linewidths=1, font_size=15)
plt.title('网络图示例')
plt.show()

# 16. 象形图（此处以简单柱状图模拟象形图的直观性）
categories = ['苹果', '香蕉', '橙子']
values = [15, 30, 45]
plt.figure(figsize=(8, 4))
bars = plt.bar(categories, values, color=['red', 'yellow', 'orange'])
for bar in bars:
    height = bar.get_height()
    plt.text(bar.get_x() + bar.get_width()/2., height,
             f'{height}',
             ha='center', va='bottom')
plt.title('象形图示例（柱状图模拟）')
plt.xlabel('水果')
plt.ylabel('数量')
plt.show()


