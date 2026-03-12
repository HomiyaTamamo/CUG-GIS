#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "TIN.h"
#include "Point.h"
#include "MapView.h"  // 使用 MapView 来显示 TIN 数据

class MainWindow : public QMainWindow {
    Q_OBJECT

public:
    explicit MainWindow(QWidget* parent = nullptr);
    ~MainWindow();

private:
    // 设置视图和布局
    void setupView();

    // 从 CSV 文件加载点数据
    void loadPointsFromCSV(const QString& filename);

    // 视图部分
    MapView* mapView;   // 用于显示 TIN 的视图
    TIN tin;            // TIN 数据模型
};

#endif // MAINWINDOW_H
