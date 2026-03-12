#########绘制V图/泰森多边形


import numpy as np
import matplotlib.pyplot as plt
from scipy.spatial import Voronoi, voronoi_plot_2d

# 设置随机种子以便结果可复现
np.random.seed(42)

# 生成10个随机点
points = np.random.rand(10, 2)

# 创建Voronoi图对象
vor = Voronoi(points)

# 绘制Voronoi图
fig, ax = plt.subplots(figsize=(8, 8))
voronoi_plot_2d(vor, ax=ax, show_vertices=False, line_colors='blue', line_width=1, point_size=10)

# 绘制原始点
ax.plot(points[:, 0], points[:, 1], 'ko')  # 黑色圆点表示原始点
ax.set_title('Voronoi Diagram with 10 Points', fontsize=14)
ax.set_xlim(0, 1)
ax.set_ylim(0, 1)
ax.set_aspect('equal')

plt.show()