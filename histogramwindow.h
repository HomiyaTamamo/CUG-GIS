#ifndef HISTOGRAMWINDOW_H
#define HISTOGRAMWINDOW_H

#include <QDialog>
#include <QImage>

namespace Ui {
    class HistogramWindow;
}

class HistogramWindow : public QDialog
{
    Q_OBJECT

public:
    explicit HistogramWindow(const QImage& image, QWidget* parent = nullptr);
    ~HistogramWindow();

private slots:
    void on_closeButton_clicked();

private:
    Ui::HistogramWindow* ui;
    QImage image;
    void calculateHistogram();
};

#endif // HISTOGRAMWINDOW_H



