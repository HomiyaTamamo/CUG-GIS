##########面可视化##########

import pandas as pd
import geopandas as gpd
import matplotlib.pyplot as plt
from scipy.interpolate import griddata
import numpy as np

# 设置中文支持
plt.rcParams['font.sans-serif'] = ['SimHei']
plt.rcParams['axes.unicode_minus'] = False

# 读取CSV文件
df = pd.read_csv('hubei_cities.csv', encoding='utf-8')

# 打印列名查看真实字段名
print("CSV列名：", df.columns.tolist())

# 将数据转换为GeoDataFrame（点数据）
gdf_points = gpd.GeoDataFrame(df, geometry=gpd.points_from_xy(df.经度, df.纬度))

# 读取湖北省各市的面边界Shapefile
boundary_gdf = gpd.read_file('湖北省_市界.shp')

# 打印Shapefile字段名
print("Shapefile字段名：", boundary_gdf.columns.tolist())

# === 修改字段名以匹配 ===
# 假设 df 中城市列为 '城市'，而 boundary_gdf 中为 'Name'
city_col_csv = '城市'
city_col_shp = 'Name'

# 重命名boundary_gdf的字段以匹配CSV
boundary_gdf = boundary_gdf.rename(columns={city_col_shp: city_col_csv})

# 合并属性数据（人口、GDP）
boundary_gdf = boundary_gdf.merge(df[[city_col_csv, '人口', 'GDP']], on=city_col_csv, how='left')

# 创建图形：人口面渲染图
fig, ax = plt.subplots(1, 1, figsize=(12, 10))
boundary_gdf.plot(column='人口', cmap='OrRd', edgecolor='k', linewidth=0.5, ax=ax, legend=True,
                   legend_kwds={'label': "人口数量",
                                'orientation': "horizontal"})

# 添加城市标注
for idx, row in gdf_points.iterrows():
    ax.annotate(text=row[city_col_csv], xy=(row.geometry.x, row.geometry.y),
                xytext=(5, 5), textcoords='offset points', fontsize=8)

ax.set_title("湖北省各市人口分布面图")
plt.axis('equal')
plt.tight_layout()
plt.show()

# --- 第二部分：基于GDP的空间插值热力图 ---

# 提取经纬度和GDP作为插值依据
lon = gdf_points.geometry.x.values
lat = gdf_points.geometry.y.values
gdp_values = gdf_points['GDP'].values

# 创建插值网格
xi = np.linspace(min(lon), max(lon), 200)
yi = np.linspace(min(lat), max(lat), 200)
zi = griddata((lon, lat), gdp_values, (xi[None, :], yi[:, None]), method='linear')

# 创建图形：GDP插值热力图
fig, ax = plt.subplots(figsize=(12, 10))
boundary_gdf.boundary.plot(ax=ax, color='black', linewidth=0.7)  # 绘制城市边界轮廓

# 显示插值热力图
contourf = ax.contourf(xi, yi, zi, levels=100, cmap='YlGnBu', alpha=0.7)
plt.colorbar(contourf, label='GDP (亿元)', shrink=0.7)

# 标注城市点
gdf_points.plot(ax=ax, color='red', markersize=30)
for idx, row in gdf_points.iterrows():
    ax.annotate(text=row[city_col_csv], xy=(row.geometry.x, row.geometry.y),
                xytext=(5, 5), textcoords='offset points', fontsize=8)

ax.set_title("湖北省各市GDP空间插值热力图")
plt.axis('equal')
plt.tight_layout()
plt.show()