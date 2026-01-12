#include "MainWindow.h"
#include "TIN.h"
#include "Point.h"
#include "MapView.h"  
#include <QApplication>
#include <QFile>
#include <QTextStream>
#include <QMessageBox>
#include <QDebug>
#include <QVBoxLayout>
#include <QWidget>

MainWindow::MainWindow(QWidget* parent)
    : QMainWindow(parent), view(new MapView(this)) {

    // 设置窗口标题
    setWindowTitle("TIN Builder");

    // 设置视图
    setupView();

    // 加载数据
    loadPointsFromCSV("TIN_data.csv");

    // 构建TIN
    tin.buildTIN();

    // 设置TIN视图
    view->setMap(tin);
}

MainWindow::~MainWindow() {}

void MainWindow::loadPointsFromCSV(const QString& filename) {
    QFile file(filename);
    if (!file.open(QIODevice::ReadOnly)) {
        QMessageBox::critical(this, "Error", "Failed to open CSV file.");
        return;
    }

    QTextStream in(&file);
    QString line;
    bool firstLine = true;

    // 读取 CSV 数据
    while (in.readLineInto(&line)) {
        // 跳过 CSV 的表头
        if (firstLine) {
            firstLine = false;
            continue;
        }

        // 将数据按逗号分隔
        QStringList data = line.split(",");
        if (data.size() != 4) continue;

        // 解析数据
        int objectId = data[0].toInt();  // OBJECTID
        QString name = data[1];          // POI 名称
        float locationX = data[2].toFloat(); // 经度
        float locationY = data[3].toFloat(); // 纬度

        // 创建 Point 对象并添加到 TIN 对象中
        Point point(objectId, name, locationX, locationY);
        tin.addPoint(point);  // 将点添加到 TIN 对象
    }

    // 构建 TIN
    tin.buildTIN();
    file.close();

    // 更新视图
    view->setTIN(tin);  // 这里视图将展示所有点
}

void MainWindow::setupView() {
    QWidget* centralWidget = new QWidget(this);
    QVBoxLayout* layout = new QVBoxLayout(centralWidget);
    layout->addWidget(view);
    setCentralWidget(centralWidget);
}
