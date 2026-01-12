###001-地理空间信息可视化-点数据#########


import pandas as pd
import matplotlib.pyplot as plt
import geopandas as gpd
from shapely.geometry import Point
import numpy as np
from scipy.interpolate import griddata
from matplotlib.colors import Normalize
import matplotlib.cm as cm

# 读取CSV文件
try:
    data = pd.read_csv('hubei_cities.csv', encoding='utf-8')
except UnicodeDecodeError:
    try:
        data = pd.read_csv('hubei_cities.csv', encoding='gbk')
    except UnicodeDecodeError:
        data = pd.read_csv('hubei_cities.csv', encoding='gb18030')

# 打印列名以确认
print("数据框列名:", data.columns.tolist())

# 检查必要列是否存在
required_columns = ['城市', '经度', '纬度', '人口', 'GDP', '人均GDP']
for col in required_columns:
    if col not in data.columns:
        raise ValueError(f"CSV文件中缺少必要列: {col}")

# 设置中文显示
plt.rcParams['font.sans-serif'] = ['SimHei']
plt.rcParams['axes.unicode_minus'] = False

# 加载湖北省地图数据
world = gpd.read_file("湖北省_省界.shp")

# 创建GeoDataFrame
gdf = gpd.GeoDataFrame(data, geometry=[Point(xy) for xy in zip(data['经度'], data['纬度'])])

# 定义分级颜色映射
cmap = plt.get_cmap('viridis', 5)  # 使用viridis颜色映射，分为5级
norm = Normalize(vmin=data['GDP'].min(), vmax=data['GDP'].max())

# 1. 分级散点图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级散点图
scatter = ax.scatter(data['经度'], data['纬度'], c=data['GDP'], cmap=cmap, norm=norm, s=100)

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级散点图')
plt.grid(True)
plt.show()

# 2. 分级热力图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级热力图
scatter = ax.scatter(data['经度'], data['纬度'], c=data['GDP'], cmap=cmap, norm=norm, s=100)

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级热力图')
plt.grid(True)
plt.show()

# 3. 分级亮点图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级亮点图
scatter = ax.scatter(data['经度'], data['纬度'], c=data['GDP'], cmap=cmap, norm=norm, s=data['人口']/10, alpha=0.7)

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市人口与GDP分级亮点图')
plt.grid(True)
plt.show()

# 4. 分级符号地图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级符号地图
gdf.plot(ax=ax, marker='o', color='red', markersize=data['GDP']/50, label='GDP')

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级符号地图')
plt.legend()
plt.grid(True)
plt.show()

# 5. 分级气泡图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级气泡图
scatter = ax.scatter(data['经度'], data['纬度'], s=data['GDP'], c=data['GDP'], cmap=cmap, norm=norm, alpha=0.6, edgecolors='w', linewidth=1)

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级气泡图')
plt.grid(True)
plt.show()

# 6. 分级网格热力图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级网格热力图
grid_x, grid_y = np.meshgrid(np.linspace(data['经度'].min(), data['经度'].max(), 50),
                             np.linspace(data['纬度'].min(), data['纬度'].max(), 50))
grid_z = griddata((data['经度'], data['纬度']), data['GDP'], (grid_x, grid_y), method='cubic')
contour = ax.contourf(grid_x, grid_y, grid_z, levels=14, cmap='RdYlBu_r')

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级网格热力图')
plt.grid(True)
plt.show()

# 7. 分级复合图表地图（带地图底图和城市名称标签）
fig, ax = plt.subplots(figsize=(10, 6))
world.plot(ax=ax, color='lightgrey', edgecolor='black')

# 绘制分级复合图表地图
gdf.plot(ax=ax, marker='o', color='red', markersize=data['GDP']/50, label='GDP')
scatter = ax.scatter(data['经度'], data['纬度'], c=data['GDP'], cmap=cmap, norm=norm, s=100, alpha=0.5)

# 添加城市名称标签
for x, y, label in zip(data['经度'], data['纬度'], data['城市']):
    ax.text(x, y, label, fontsize=9, ha='center', va='bottom', color='black')

# 添加颜色条（图例）
sm = cm.ScalarMappable(cmap=cmap, norm=norm)
sm.set_array([])
plt.colorbar(sm, ax=ax, label='GDP (亿元)')

plt.xlabel('经度')
plt.ylabel('纬度')
plt.title('湖北省地级市GDP分级复合图表地图')
plt.legend()
plt.grid(True)
plt.show()