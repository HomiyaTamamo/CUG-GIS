#include "histogramwindow.h"
#include "ui_histogramwindow.h"
#include <QPainter>

HistogramWindow::HistogramWindow(const QImage& image, QWidget* parent) :
    QDialog(parent),
    ui(new Ui::HistogramWindow),
    image(image)
{
    ui->setupUi(this);
    calculateHistogram();
}

HistogramWindow::~HistogramWindow()
{
    delete ui;
}

void HistogramWindow::calculateHistogram()
{
    // 计算直方图数据
    QVector<int> histogram(256, 0);  // 假设灰度有256个bin

    for (int y = 0; y < image.height(); ++y) {
        for (int x = 0; x < image.width(); ++x) {
            QRgb pixel = image.pixel(x, y);
            int grayValue = qGray(pixel);
            histogram[grayValue]++;
        }
    }

    // 找到直方图中的最大值以进行归一化
    int maxCount = 0;
    for (int i = 0; i < 256; ++i) {
        if (histogram[i] > maxCount) {
            maxCount = histogram[i];
        }
    }

    // 在 ui->histogramWidget 上绘制直方图（例如 QLabel）
    QPixmap pixmap(ui->histogramWidget->size());
    pixmap.fill(Qt::white);
    QPainter painter(&pixmap);
    painter.setPen(Qt::black);

    int barWidth = pixmap.width() / 256;
    qreal scale = pixmap.height() / qreal(maxCount);

    for (int i = 0; i < 256; ++i) {
        int barHeight = int(histogram[i] * scale);
        painter.drawLine(i * barWidth, pixmap.height(), i * barWidth, pixmap.height() - barHeight);
    }

    ui->histogramWidget->setPixmap(pixmap);
}

void HistogramWindow::on_closeButton_clicked()
{
    close();
}
