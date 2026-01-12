########栅格可视化----三维#########

import rasterio
import numpy as np
import pyvista as pv
from mayavi import mlab
import plotly.graph_objects as go

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
# 使用 PyVista 进行三维可视化
# ========================
def plot_with_pyvista():
    # 创建 PyVista 网格
    grid = pv.StructuredGrid(xx_sub, yy_sub, dem_sub)

    # 绘制三维 DEM
    plotter = pv.Plotter()
    plotter.add_mesh(grid, cmap='terrain', show_edges=False)
    plotter.show_grid()
    plotter.set_background('white')
    plotter.add_scalar_bar(title='高程 (m)')
    plotter.show()


# ========================
# 使用 Mayavi 进行三维可视化
# ========================
def plot_with_mayavi():
    # 绘制三维 DEM
    mlab.figure(size=(1200, 900), bgcolor=(1, 1, 1))
    surf = mlab.surf(xx_sub, yy_sub, dem_sub, colormap='terrain')

    # 添加颜色条
    mlab.colorbar(surf, title='高程 (m)', orientation='vertical')

    # 设置视角
    mlab.view(azimuth=-120, elevation=40)

    # 添加标题和坐标轴
    mlab.title('湖北省 DEM - 三维地形图（Mayavi）')
    mlab.xlabel('经度')
    mlab.ylabel('纬度')
    mlab.zlabel('高程 (m)')

    mlab.show()


# ========================
# 使用 Plotly 进行三维可视化
# ========================
def plot_with_plotly():
    # 绘制三维 DEM
    fig = go.Figure(data=[go.Surface(z=dem_sub, x=xx_sub, y=yy_sub, colorscale='Viridis')])

    # 设置布局
    fig.update_layout(
        title='湖北省 DEM - 三维地形图（Plotly）',
        scene=dict(
            xaxis_title='经度',
            yaxis_title='纬度',
            zaxis_title='高程 (m)',
            camera=dict(
                up=dict(x=0, y=0, z=1),
                center=dict(x=0, y=0, z=0),
                eye=dict(x=1.25, y=1.25, z=1.25)
            )
        ),
        width=1200,
        height=900,
        margin=dict(l=65, r=50, b=65, t=90)
    )

    fig.show()


# 调用函数进行可视化
plot_with_pyvista()
plot_with_mayavi()
plot_with_plotly()