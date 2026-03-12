#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QGraphicsScene>
#include <QGraphicsView>
#include <QPushButton>
#include <QLineEdit>
#include <QLabel>
#include <QFileDialog>
#include <QTextStream>
#include <QVector>
#include <QPointF>
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QWidget>
#include <QWheelEvent>  // 需要包含此头文件
#include "JsonDataLoader.h"

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget* parent = nullptr);
    ~MainWindow();
    void visualizePointsFromJson();
    void displayInputPoint(const QPointF& point);

    double getElevationForPoint(const QPointF& point);

    void drawConvexHull();

    QVector<QPointF> computeConvexHull(const QVector<QPointF>& points);

    double crossProduct(const QPointF& a, const QPointF& b, const QPointF& c);
    QPointF projectCoordinatesToScreen(double longitude, double latitude);

private slots:
    void on_loadButton_clicked();
    void on_calculateButton_clicked();
    void on_addPointButton_clicked();
protected:
    void wheelEvent(QWheelEvent* event) override;  // 放大和缩小

private:
    QWidget* centralWidget;
    QGraphicsScene* scene;
    QVector<QPointF> points;  // 存储输入的点
    QVector<double> values;   // 存储输入点的高程值
    QPointF targetPoint;      // 插值目标点
    QVector<QGraphicsTextItem*> textItems;  // 声明 textItems 存储文本项
    QLineEdit* targetXLineEdit;
    QLineEdit* targetYLineEdit;
    QLineEdit* mLineEdit;
    QLineEdit* pLineEdit;
    QLabel* resultLabel;
    void loadTxtData(const QString& filename);  // 从文本文件加载数据
    JsonDataLoader jsonLoader;  // 创建 JsonDataLoader 对象
    double idwInterpolation(const QPointF& target, int m, double p);  // IDW 插值计算
    void visualizePoints();  // 可视化数据点
    void adjustViewScale();  // 调整视图比例尺
};

#endif // MAINWINDOW_H
