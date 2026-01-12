# 首先运行安装必要库（如果尚未安装）
# !pip install geopandas pandas plotly matplotlib ipywidgets pillow
import matplotlib
from jedi.api.refactoring import inline

# 初始化Jupyter环境

import plotly.io as pio

pio.renderers.default = 'notebook'  # 设置Plotly在Notebook内显示

import os
import geopandas as gpd
import pandas as pd
import plotly.express as px
import matplotlib.pyplot as plt
import matplotlib.animation as animation
from matplotlib import ticker
import ipywidgets as widgets
from IPython.display import display, HTML, clear_output

# 配置参数
SHP_PATH = "湖北省_市界.shp"  # 确保路径正确
CSV_PATH = "hubei_cities_years.csv"
OUTPUT_DIR = "output/"

# 创建输出目录
if not os.path.exists(OUTPUT_DIR):
    os.makedirs(OUTPUT_DIR)


def load_data():
    """加载并预处理数据"""
    try:
        gdf = gpd.read_file(SHP_PATH)
        df = pd.read_csv(CSV_PATH)
        merged = gdf.merge(df, left_on='Name', right_on='城市')
        print("数据加载成功！")
        return merged, df
    except Exception as e:
        print(f"数据加载失败: {str(e)}")
        return None, None


def plot_choropleth(merged, show=True):
    """地理分布动画"""
    # 直接使用__geo_interface__获取地理数据
    geojson = merged.set_index('城市').geometry.__geo_interface__

    fig = px.choropleth(
        merged,
        geojson=geojson,
        locations='城市',
        color='GDP',
        animation_frame='年份',
        color_continuous_scale='Viridis',
        range_color=(0, merged['GDP'].max()),
        title='湖北省各城市GDP年度变化'
    )
    fig.update_geos(fitbounds="locations", visible=False)
    fig.write_html(OUTPUT_DIR + "choropleth.html")
    if show:
        display(HTML("<h4>地理分布动画（已保存到output目录）</h4>"))
        fig.show()
    return fig


def plot_3d_tower(df, show=True):
    """3D时间塔可视化"""
    fig = px.scatter_3d(
        df,
        x='经度',
        y='纬度',
        z='年份',
        size='GDP',
        color='城市',
        animation_frame='年份',
        size_max=50,
        title='3D时间塔 - GDP发展'
    )
    fig.write_html(OUTPUT_DIR + "3d_tower.html")
    if show:
        display(HTML("<h4>3D时间塔（已保存到output目录）</h4>"))
        fig.show()
    return fig


def plot_heatmap(df, show=True):
    """热图演变"""
    fig = px.density_mapbox(
        df,
        lat='纬度',
        lon='经度',
        z='GDP',
        radius=30,
        center=dict(lat=30.5, lon=112),
        zoom=5.5,
        mapbox_style='stamen-terrain',
        animation_frame='年份',
        title='GDP热力演变图'
    )
    fig.write_html(OUTPUT_DIR + "heatmap.html")
    if show:
        display(HTML("<h4>热力演变图（已保存到output目录）</h4>"))
        fig.show()
    return fig


def plot_bar_race(df, show=True):
    """动态条形图竞赛"""
    df_sorted = df.sort_values(['年份', 'GDP'], ascending=[True, False])
    years = df_sorted['年份'].unique()

    fig, ax = plt.subplots(figsize=(12, 8))

    def update(year):
        ax.clear()
        data = df_sorted[df_sorted['年份'] == year]
        ax.barh(data['城市'], data['GDP'], color='#1f77b4')
        ax.set_title(f'湖北省各城市GDP排名 - {year}')
        ax.xaxis.set_major_formatter(ticker.StrMethodFormatter('{x:,.0f}亿'))
        ax.invert_yaxis()
        plt.box(False)

    ani = animation.FuncAnimation(fig, update, frames=years, interval=800)

    # 保存GIF
    ani.save(OUTPUT_DIR + "bar_race.gif", writer='pillow')

    # 在Notebook中显示
    if show:
        display(HTML("<h4>动态条形图竞赛（已保存到output目录）</h4>"))
        plt.close()
        display(HTML(ani.to_jshtml()))
    return ani


def plot_layered(df, show=True):
    """分层叠加可视化"""
    import plotly.graph_objects as go

    fig = go.Figure()
    years = df['年份'].unique()

    for year in years:
        year_df = df[df['年份'] == year]
        fig.add_trace(go.Scattergeo(
            lon=year_df['经度'],
            lat=year_df['纬度'],
            text=year_df.apply(lambda x: f"{x['城市']}<br>GDP: {x['GDP']}亿", axis=1),
            marker=dict(
                size=year_df['GDP'] / 50,
                color=year_df['年份'],
                colorscale='Viridis',
                showscale=True
            ),
            name=str(year),
            visible=(year == years[0])
        ))

    fig.update_geos(
        resolution=50,
        showcoastlines=True,
        coastlinecolor="RebeccaPurple",
        showland=True,
        landcolor="LightGreen",
        center=dict(lon=112, lat=31),
        projection_scale=6
    )

    steps = []
    for i, year in enumerate(years):
        step = dict(
            method="update",
            args=[{"visible": [False] * len(years) + [True]},
                  {"title": f"年份: {year}"}],
            label=str(year)
        )
        steps.append(step)

    sliders = [dict(
        active=0,
        currentvalue={"prefix": "年份: "},
        pad={"t": 50},
        steps=steps
    )]

    fig.update_layout(
        title='GDP分层叠加可视化',
        sliders=sliders,
        height=600
    )
    fig.write_html(OUTPUT_DIR + "layered.html")
    if show:
        display(HTML("<h4>分层叠加可视化（已保存到output目录）</h4>"))
        fig.show()
    return fig


# 主界面
def main_gui():
    merged, df = load_data()
    if merged is None or df is None:
        return

    # 创建交互菜单
    menu = widgets.Dropdown(
        options=[
            ('地理分布动画', 1),
            ('3D时间塔', 2),
            ('热图演变', 3),
            ('条形图竞赛', 4),
            ('分层叠加', 5),
            ('全部可视化', 0)
        ],
        description='选择可视化类型:',
        style={'description_width': 'initial'},
        layout={'width': '500px'}
    )

    output = widgets.Output()

    display(widgets.VBox([
        widgets.HTML("<h2>湖北省城市发展可视化工具</h2>"),
        menu,
        output
    ]))

    def on_change(change):
        with output:
            clear_output(wait=True)
            if change['new'] == 1:
                plot_choropleth(merged)
            elif change['new'] == 2:
                plot_3d_tower(df)
            elif change['new'] == 3:
                plot_heatmap(df)
            elif change['new'] == 4:
                plot_bar_race(df)
            elif change['new'] == 5:
                plot_layered(df)
            elif change['new'] == 0:
                plot_choropleth(merged, show=False)
                plot_3d_tower(df, show=False)
                plot_heatmap(df, show=False)
                plot_bar_race(df, show=False)
                plot_layered(df, show=False)
                display(HTML("<h3 style='color:green'>所有可视化已生成到output目录！</h3>"))
                display(HTML("<p>包含以下文件：<br>" +
                             "<br>".join(os.listdir(OUTPUT_DIR)) + "</p>"))

    menu.observe(on_change, names='value')


# 启动界面
main_gui()