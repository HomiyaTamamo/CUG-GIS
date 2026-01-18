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
# 图一：黑白灰度图
# ========================
plt.figure(figsize=(10, 8))
plt.imshow(dem, cmap='gray', origin='upper')
plt.colorbar(label='高程 (m)')
plt.title('湖北省 DEM - 黑白灰度图')
plt.axis('off')
plt.tight_layout()
plt.show()

# ========================
# 图二：彩色地形图
# ========================
plt.figure(figsize=(10, 8))
plt.imshow(dem, cmap='terrain', origin='upper')
plt.colorbar(label='高程 (m)')
plt.title('湖北省 DEM - 彩色地形图')
plt.axis('off')
plt.tight_layout()
plt.show()

# ========================
# 图三：热力图（伪彩色）
# ========================
plt.figure(figsize=(10, 8))
plt.imshow(dem, cmap='jet', origin='upper')
plt.colorbar(label='高程 (m)')
plt.title('湖北省 DEM - 热力图')
plt.axis('off')
plt.tight_layout()
plt.show()



