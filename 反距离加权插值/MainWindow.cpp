#include "mainwindow.h"
#include <QApplication>
#include <QFileDialog>
#include <QGraphicsEllipseItem>
#include <QGraphicsTextItem>
#include <QVector>
#include <QPointF>
#include <cmath>
#include <QMessageBox>
#include "JsonDataLoader.h"
#include <QGraphicsView>
#include <QPushButton>
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QLabel>
#include <QLineEdit>
#include <QDebug>

MainWindow::MainWindow(QWidget* parent)
    : QMainWindow(parent)
{
    centralWidget = new QWidget(this);
    setCentralWidget(centralWidget);

    scene = new QGraphicsScene(this);
    QGraphicsView* view = new QGraphicsView(scene, this);
    view->setRenderHint(QPainter::Antialiasing);
    view->setRenderHint(QPainter::TextAntialiasing);

    view->setDragMode(QGraphicsView::ScrollHandDrag);
    view->setInteractive(true);

    QVBoxLayout* mainLayout = new QVBoxLayout(centralWidget);

    QHBoxLayout* inputLayout = new QHBoxLayout();
    inputLayout->addWidget(new QLabel("Target Latitude:"));
    targetXLineEdit = new QLineEdit(this);
    inputLayout->addWidget(targetXLineEdit);

    inputLayout->addWidget(new QLabel("Target Longitude:"));
    targetYLineEdit = new QLineEdit(this);
    inputLayout->addWidget(targetYLineEdit);

    mainLayout->addLayout(inputLayout);

    resultLabel = new QLabel(this);
    mainLayout->addWidget(resultLabel);

    QPushButton* loadButton = new QPushButton("Load Data", this);
    mainLayout->addWidget(loadButton);

    QPushButton* calculateButton = new QPushButton("Calculate Interpolation", this);
    mainLayout->addWidget(calculateButton);

    QPushButton* addPointButton = new QPushButton("Add Point", this);
    mainLayout->addWidget(addPointButton);

    mainLayout->addWidget(view);

    connect(loadButton, &QPushButton::clicked, this, &MainWindow::on_loadButton_clicked);
    connect(calculateButton, &QPushButton::clicked, this, &MainWindow::on_calculateButton_clicked);
    connect(addPointButton, &QPushButton::clicked, this, &MainWindow::on_addPointButton_clicked);

    setWindowTitle("IDW Interpolation");
    resize(800, 600);
}


MainWindow::~MainWindow() {}

void MainWindow::on_loadButton_clicked()
{
    // 导入文件，txt或json格式
    QString filename = QFileDialog::getOpenFileName(this, "Open Data File", "", "Text Files (*.txt);;JSON Files (*.json);;All Files (*)");
    if (!filename.isEmpty()) {
        if (filename.endsWith(".txt", Qt::CaseInsensitive)) {
            loadTxtData(filename);  
            visualizePoints();       
        }
        else if (filename.endsWith(".json", Qt::CaseInsensitive)) {
            if (jsonLoader.loadJsonData(filename, scene)) {
                qDebug() << "JSON Data loaded successfully";
            }
            else {
                qWarning() << "Failed to load JSON data";
            }
            visualizePointsFromJson();  
        }
        adjustViewScale();  
    }
}

void MainWindow::on_calculateButton_clicked()
{
    // 绘制凸包（如果有三个及以上点）
    drawConvexHull();
}
QVector<QPointF> points; 

void MainWindow::on_addPointButton_clicked()
{
    // 获取输入框中的经纬度坐标
    double latitude = targetXLineEdit->text().toDouble();
    double longitude = targetYLineEdit->text().toDouble();

    // 根据经纬度进行缩放，转换为屏幕坐标
    QPointF projectedPoint = projectCoordinatesToScreen(longitude, latitude);

    // 将点添加到 points 向量中
    points.append(projectedPoint);

    // 将点显示在场景中
    displayInputPoint(projectedPoint);
}


QPointF MainWindow::projectCoordinatesToScreen(double longitude, double latitude)
{
    // 简单的线性缩放，将经纬度转换为屏幕坐标
    double x = longitude * 40000;  // 缩放因子（可根据需要调整）
    double y = latitude * 40000;   // 缩放因子（可根据需要调整）

    return QPointF(x, y);
}

void MainWindow::displayInputPoint(const QPointF& point)
{
    qDebug() << "Adding point at (" << point.x() << ", " << point.y() << ")";

    // 创建一个圆形项表示输入的点，并将其添加到场景中
    QGraphicsEllipseItem* pointItem = scene->addEllipse(point.x() - 5, point.y() - 5, 10, 10, QPen(Qt::red), QBrush(Qt::red));
    pointItem->setZValue(10);  // 确保点位于其他图形上方
    scene->update();  // Force scene update to refresh the view
}




void MainWindow::drawConvexHull()
{
    // 检查点的数量是否足够
    if (points.size() < 3) {
        QMessageBox::warning(this, "数据错误", "数据点数量不足，无法计算凸包！");
        return;
    }

    // 计算凸包
    QVector<QPointF> convexHullPoints = computeConvexHull(points);

    // 创建一个多边形来显示凸包
    QPolygonF hullPolygon;
    for (const QPointF& pt : convexHullPoints) {
        hullPolygon.append(pt);
    }

    // 在场景中绘制凸包
    QGraphicsPolygonItem* hullItem = scene->addPolygon(hullPolygon, QPen(Qt::green), QBrush(Qt::transparent));
    hullItem->setZValue(1); // 确保凸包绘制在点集之上
}

QVector<QPointF> MainWindow::computeConvexHull(const QVector<QPointF>& points)
{
    // 至少三个点才能绘制凸包
    if (points.size() < 3) {
        return {};
    }

    // 按 x 坐标对点进行排序（如果 x 相等，则按 y 坐标排序）
    QVector<QPointF> sortedPoints = points;
    std::sort(sortedPoints.begin(), sortedPoints.end(), [](const QPointF& a, const QPointF& b) {
        return a.x() < b.x() || (a.x() == b.x() && a.y() < b.y());
        });

    // 用于计算叉积
    auto crossProduct = [](const QPointF& o, const QPointF& a, const QPointF& b) {
        return (a.x() - o.x()) * (b.y() - o.y()) - (a.y() - o.y()) * (b.x() - o.x());
        };

    // 凸包下部构建
    QVector<QPointF> lowerHull;
    for (const QPointF& pt : sortedPoints) {
        while (lowerHull.size() >= 2 && crossProduct(lowerHull[lowerHull.size() - 2], lowerHull.last(), pt) <= 0) {
            lowerHull.pop_back();
        }
        lowerHull.append(pt);
    }

    // 凸包上部构建
    QVector<QPointF> upperHull;
    for (int i = sortedPoints.size() - 1; i >= 0; --i) {
        while (upperHull.size() >= 2 && crossProduct(upperHull[upperHull.size() - 2], upperHull.last(), sortedPoints[i]) <= 0) {
            upperHull.pop_back();
        }
        upperHull.append(sortedPoints[i]);
    }

    // 删除重复点
    lowerHull.pop_back();
    upperHull.pop_back();

    // 得到凸包
    lowerHull.append(upperHull);

    return lowerHull;
}


double MainWindow::crossProduct(const QPointF& p1, const QPointF& p2, const QPointF& p3)
{
    return (p2.x() - p1.x()) * (p3.y() - p1.y()) - (p2.y() - p1.y()) * (p3.x() - p1.x());
}

void MainWindow::loadTxtData(const QString& filename)
{
    QFile file(filename);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text)) {
        QMessageBox::critical(this, "Error", "Unable to open file.");
        return;
    }

    QTextStream in(&file);
    int n, m;
    in >> n >> m;  

    points.clear();
    values.clear();

    for (int i = 0; i < n; ++i) {
        double x, y, value;
        in >> x >> y >> value;
        points.append(QPointF(x, y));
        values.append(value);
    }

    file.close();
}

double MainWindow::idwInterpolation(const QPointF& target, int m, double p)
{
    if (m > points.size()) {
        qCritical() << "Error: m exceeds the number of points!";
        return 0.0;  // 或返回合适的默认值
    }

    QVector<QPair<double, double>> distances;

    // 计算目标点到每个数据点的距离
    for (int i = 0; i < points.size(); ++i) {
        double dist = std::sqrt(std::pow(target.x() - points[i].x(), 2) + std::pow(target.y() - points[i].y(), 2));

        if (dist == 0) {
            return values[i];
        }

        distances.append(qMakePair(dist, values[i]));
    }

    std::sort(distances.begin(), distances.end(), [](const QPair<double, double>& a, const QPair<double, double>& b) {
        return a.first < b.first;
        });

    double numerator = 0.0;
    double denominator = 0.0;

    for (int i = 0; i < m; ++i) {
        double dist = distances[i].first;
        double value = distances[i].second;

        double weight = 1.0 / std::pow(dist, p);
        numerator += weight * value;
        denominator += weight;
    }

    return numerator / denominator;
}

double MainWindow::getElevationForPoint(const QPointF& point)
{
    //假设海拔基于加载的 JSON 数据并使用它来查找最近点的海拔。
    return 100.0; 
}




void MainWindow::visualizePointsFromJson() {
    if (scene == nullptr) return;

    scene->clear();  // 清空场景

    // 获取从 JSON 加载的等高线数据
    const QVector<QVector<QPointF>>& contours = jsonLoader.getContours();
    const QVector<double>& elevations = jsonLoader.getElevations();

    qDebug() << "Loaded " << contours.size() << " contour(s) from JSON.";

    // 遍历所有等高线并绘制
    for (int i = 0; i < contours.size(); ++i) {
        const QVector<QPointF>& contour = contours[i];
        double elevation = elevations[i];

        qDebug() << "Drawing contour with elevation: " << elevation;

        // 为每条等高线创建 QGraphicsLineItem
        for (int j = 0; j < contour.size() - 1; ++j) {
            const QPointF& startPoint = contour[j];
            const QPointF& endPoint = contour[j + 1];

            // 使用 QGraphicsLineItem 绘制等高线
            QGraphicsLineItem* line = scene->addLine(startPoint.x(), startPoint.y(),
                endPoint.x(), endPoint.y(),
                QPen(Qt::blue));  // 使用蓝色绘制线段
        }
    }

    adjustViewScale();
}
void MainWindow::adjustViewScale() {
    QGraphicsView* view = this->findChild<QGraphicsView*>();  // 获取 QGraphicsView
    if (!view) return;

    // 获取场景的边界框
    QRectF sceneRect = scene->sceneRect();
    view->setSceneRect(sceneRect);

    // 根据场景的尺寸调整视图的缩放
    qreal scaleFactor = view->transform().m11();
    qreal minScale = 0.1;
    qreal maxScale = 20;

    if (scaleFactor < minScale) {
        view->resetMatrix();
        view->scale(minScale, minScale);
    }
    else if (scaleFactor > maxScale) {
        view->resetMatrix();
        view->scale(maxScale, maxScale);
    }
}
void MainWindow::visualizePoints()
{
    if (scene == nullptr) return;

    scene->clear();
    textItems.clear();

    QGraphicsView* view = this->findChild<QGraphicsView*>();
    if (!view) return;
    qreal scaleFactor = view->transform().m11();

    qreal pointRadius = 0.5 * scaleFactor;
    pointRadius = qBound(0.1, pointRadius, 1.0);

    for (int i = 0; i < points.size(); ++i) {
        const QPointF& point = points[i];
        double value = values[i];

        QGraphicsEllipseItem* ellipse = scene->addEllipse(point.x() - pointRadius, point.y() - pointRadius, 2 * pointRadius, 2 * pointRadius);
        ellipse->setBrush(Qt::blue);
        ellipse->setPen(QPen(Qt::black));

        QGraphicsTextItem* textItem = scene->addText(QString::number(value));
        textItem->setPos(point.x() + pointRadius + 0.000001, point.y() - pointRadius - 0.000001);
        textItem->setScale(scaleFactor);  // 调整文本大小
        textItems.append(textItem);
    }
}
void MainWindow::wheelEvent(QWheelEvent* event)
{
    QGraphicsView* view = this->findChild<QGraphicsView*>();  // 获取 QGraphicsView
    if (!view) return;

    // 获取鼠标滚轮的增量
    int delta = event->angleDelta().y();

    // 根据滚轮的增量来缩放
    qreal scaleFactor = view->transform().m11();  // 获取当前缩放因子
    qreal factor = (delta > 0) ? 1.2 : 0.8;  // 放大或者缩小

    // 计算新的缩放因子
    scaleFactor *= factor;

    // 限制缩放因子的范围
    scaleFactor = qBound(0.5, scaleFactor, 3.0);

    // 应用新的缩放因子
    view->resetMatrix();
    view->scale(scaleFactor, scaleFactor);
}