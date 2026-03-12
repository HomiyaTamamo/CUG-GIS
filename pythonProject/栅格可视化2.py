####栅格可视化---------平面二维图

import rasterio
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# 设置中文字体支持（如需中文标题）
plt.rcParams['font.sans-serif'] = ['SimHei']
plt.rcParams['axes.unicode_minus'] = False

# 读取DEM文件
filename = '湖北DEM.tif'
with rasterio.open(filename) as src:
    dem = src.read(1).astype(np.float32)
    transform = src.transform
    crs = src.crs

# 处理nodata值
dem[dem == src.nodata] = np.nan

# 获取坐标网格
height, width = dem.shape
x_coords = np.arange(width) * transform[0] + transform[2]
y_coords = np.arange(height) * transform[4] + transform[5]
xx, yy = np.meshgrid(x_coords, y_coords)

# 降采样用于加速绘图
step = 5
xx_sub = xx[::step, ::step]
yy_sub = yy[::step, ::step]
dem_sub = dem[::step, ::step]

# ========================
# 图四：等高线与颜色填充图
# ========================
plt.figure(figsize=(10, 8))

# 等高线层级设置（每 500 米一个间隔）
contour_levels = np.arange(np.nanmin(dem), np.nanmax(dem) + 500, 500)

# 绘制等高线
c = plt.contour(xx, yy, dem, levels=contour_levels, colors='black', linewidths=0.5)

# 添加单个等高线数值标签
for idx, level in enumerate(contour_levels):
    # 找到当前等高线上的任意一点的坐标索引，这里选择每个等高线的第一个非nan点作为标签位置
    mask = ~np.isnan(dem)
    for i in range(dem.shape[0]):
        for j in range(dem.shape[1]):
            if mask[i][j] and np.isclose(dem[i][j], level, atol=1):  # atol是绝对容差
                plt.text(xx[i, j], yy[i, j], f'{int(level)}m',
                         color="red", fontsize=9, ha='left')  # 在地图上添加文本
                break  # 只需要第一个找到的点
        else:
            continue
        break

# 其他设置
plt.title('等高线图（每500米一条，每条线仅一个标签）')
plt.xlabel('X坐标')
plt.ylabel('Y坐标')
plt.axis('equal')
plt.show()


