#ifndef ZOOMGRAPHICSVIEW_H
#define ZOOMGRAPHICSVIEW_H

#include <QGraphicsView>

class CZoomGraphicsView : public QGraphicsView
{
    Q_OBJECT
public:
    explicit CZoomGraphicsView(QWidget *parent = 0);
    void resetZoom();

protected:
    void wheelEvent ( QWheelEvent * event );
    void mouseMoveEvent(QMouseEvent *event);
    void mouseDoubleClickEvent(QMouseEvent *event);


private:
    int _numScheduledScalings;
    double dCurrentScalingFactor;
    QPointF localPt;

signals:
    void mouseCoordChanged(int iX, int iY, float fZoomFactor);
    void mouseHitPoint(int iX, int iY, int iAction);

public slots:
    void scalingTime(qreal x);
    void animFinished();

};

#endif // ZOOMGRAPHICSVIEW_H
