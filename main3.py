######线数据可视化####


import pandas as pd
import geopandas as gpd
import matplotlib.pyplot as plt
from scipy.interpolate import griddata
import numpy as np

# 设置中文显示
plt.rcParams['font.sans-serif'] = ['SimHei']
plt.rcParams['axes.unicode_minus'] = False

# 读取CSV文件
df = pd.read_csv('hubei_cities.csv', encoding='utf-8')

# 将数据转换为GeoDataFrame
gdf = gpd.GeoDataFrame(df, geometry=gpd.points_from_xy(df.经度, df.纬度))

# 读取湖北省边界Shapefile
boundary = gpd.read_file('湖北省_市界.shp')

# 计算武汉到其他城市的距离并绘图
wuhan_loc = gdf[gdf['城市'] == '武汉市'].geometry.iloc[0]
gdf['distance_to_wuhan'] = gdf.geometry.distance(wuhan_loc)

# 创建地图展示武汉到其他城市的距离 + 连线 + 距离标签
fig, ax = plt.subplots(figsize=(15, 15))
boundary.boundary.plot(ax=ax, color="black", linewidth=1)  # 添加湖北省边界
gdf.plot(ax=ax, markersize=30, color='red')

wuhan_row = gdf[gdf['城市'] == '武汉市'].iloc[0]

for idx, row in gdf.iterrows():
    if row['城市'] == '武汉市':
        continue  # 跳过自己到自己的连线

    # 绘制连线
    ax.plot([wuhan_row['经度'], row['经度']],
            [wuhan_row['纬度'], row['纬度']],
            'b--', linewidth=1)

    # 计算中点位置并添加距离文本
    mid_lon = (wuhan_row['经度'] + row['经度']) / 2
    mid_lat = (wuhan_row['纬度'] + row['纬度']) / 2
    distance_km = row['distance_to_wuhan'] / 1000  # meters to km
    ax.text(mid_lon, mid_lat, f'{distance_km:.1f} km', fontsize=8, color='green')

    # 城市名称标注
    ax.annotate(text=row['城市'], xy=(row['经度'], row['纬度']),
                xytext=(7, 7), textcoords='offset points')

# 武汉单独标注
ax.annotate(text=row['城市'], xy=(row['经度'], row['纬度']), xytext=(7, 7), textcoords='offset points')

plt.title('从武汉市到各地的路径与距离')
plt.show()

# 旅游路线方向图 + 箭头
locations = {
    '黄鹤楼': (114.3053, 30.5554),
    '三峡大坝': (111.0064, 30.7791),
    '武当山': (111.0335, 32.425)
}

route_points = list(locations.values())
route_names = list(locations.keys())

fig, ax = plt.subplots(figsize=(10, 8))

# 绘制省界
boundary.boundary.plot(ax=ax, color="black", linewidth=1)
# 标注城市名称
for idx, row in gdf.iterrows():
    ax.annotate(text=row['城市'],
                xy=(row['经度'], row['纬度']),
                xytext=(7, 7),
                textcoords='offset points',
                fontsize=4,
                bbox=dict(boxstyle="round,pad=0.3", facecolor="white", edgecolor="none", alpha=0.7))
# 绘制起点和终点的点
for name, loc in locations.items():
    ax.plot(loc[0], loc[1], 'bo')  # 蓝色点
    ax.annotate(text=name, xy=loc, xytext=(7, 7), textcoords='offset points')

# 绘制箭头
for i in range(len(route_points) - 1):
    start = route_points[i]
    end = route_points[i + 1]

    dx = end[0] - start[0]
    dy = end[1] - start[1]

    ax.arrow(start[0], start[1], dx, dy,
             head_width=0.1, length_includes_head=True,
             fc='red', ec='red', linewidth=1.5)

plt.title('旅游路线方向图（带箭头）')
plt.axis('equal')  # 保持坐标轴比例一致
plt.show()

# 人口密度插值等值线图 + 等值线 + 标注数值

lon = df['经度'].values
lat = df['纬度'].values
pop_density = df['人口'].values / (1000 * 1000)  # 简单的人口密度估算（单位：人/km²）

# 创建网格用于插值
xi = np.linspace(min(lon), max(lon), 100)
yi = np.linspace(min(lat), max(lat), 100)
zi = griddata((lon, lat), pop_density, (xi[None, :], yi[:, None]), method='linear')

# 绘图
fig, ax = plt.subplots(figsize=(10, 8))

# 绘制湖北省边界作为底图
boundary.boundary.plot(ax=ax, color="black", linewidth=1)

# 绘制填充等值线（颜色填充）
contourf = ax.contourf(xi, yi, zi, 15, cmap=plt.cm.viridis, alpha=0.7)
plt.colorbar(contourf, label='人口密度（人/km²）')

# 绘制等值线（黑色实线）并添加标签
contour_lines = ax.contour(xi, yi, zi, levels=15, colors='black', linewidths=0.8)
ax.clabel(contour_lines, inline=True, fontsize=8, fmt="%.1f")

# 绘制城市点
gdf.plot(ax=ax, color='red', markersize=30)

# 标注城市名称
for idx, row in gdf.iterrows():
    ax.annotate(text=row['城市'],
                xy=(row['经度'], row['纬度']),
                xytext=(7, 7),
                textcoords='offset points',
                fontsize=9,
                bbox=dict(boxstyle="round,pad=0.3", facecolor="white", edgecolor="none", alpha=0.7))

# 设置标题与布局
plt.title('湖北省人口密度插值等值线图')
plt.tight_layout()
plt.show()