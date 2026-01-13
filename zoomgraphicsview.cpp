#include "zoomgraphicsview.h"
#include <QWheelEvent>
#include <QTimeLine>
#include <QGraphicsItem>

CZoomGraphicsView::CZoomGraphicsView(QWidget *parent) :
    QGraphicsView(parent)
{
    _numScheduledScalings = 0;
    dCurrentScalingFactor = 1.0;
    setMouseTracking(true);
}


void CZoomGraphicsView::wheelEvent ( QWheelEvent * event )
{
    int numDegrees = event->delta() / 8;
    int numSteps = numDegrees / 15; // see QWheelEvent documentation
    _numScheduledScalings += numSteps;
    if (_numScheduledScalings * numSteps < 0) // if user moved the wheel in another direction, we reset previously scheduled scalings
        _numScheduledScalings = numSteps;

    QTimeLine *anim = new QTimeLine(350, this);
    anim->setUpdateInterval(20);

    connect(anim, SIGNAL (valueChanged(qreal)), SLOT (scalingTime(qreal)));
    connect(anim, SIGNAL (finished()), SLOT (animFinished()));
    anim->start();
}

void CZoomGraphicsView::mouseMoveEvent(QMouseEvent *event)
{
    QGraphicsView::mouseMoveEvent(event);

    // get scene coords from the view coord
    QPointF scenePt = mapToScene(event->pos());

    // get the item that was clicked on
    //QGraphicsItem *item = itemAt(scenePt.toPoint()); //, transform()

    //if(item)
    //{
        // get the scene pos in the item's local coordinate space
    //    localPt = item->mapFromScene(scenePt);

        //emit mouseCoordChanged((int)localPt.x(), (int)localPt.y());

    //}
    emit mouseCoordChanged((int)scenePt.x(), (int)scenePt.y(), dCurrentScalingFactor);
}


void CZoomGraphicsView::mouseDoubleClickEvent(QMouseEvent *event)
{
    //QGraphicsView::mousePressEvent(event);

    if(event->button())
    {
        // get scene coords from the view coord
        QPointF scenePt = mapToScene(event->pos());

        emit mouseHitPoint((int)scenePt.x(), (int)scenePt.y(),1);
    }
}


void CZoomGraphicsView::scalingTime(qreal x)
{
    qreal factor = 1.0+ qreal(_numScheduledScalings) / 300.0;


    if( dCurrentScalingFactor * factor >= 0.125 && dCurrentScalingFactor * factor < 16)
    {
        dCurrentScalingFactor *= factor;
        scale(factor, factor);
    }
}

void CZoomGraphicsView::animFinished()
{
    if ( _numScheduledScalings > 0)
        _numScheduledScalings--;
    else
        _numScheduledScalings++;
    sender()->~QObject();
}

void CZoomGraphicsView::resetZoom()
{
    resetMatrix();
    dCurrentScalingFactor = 1.0;
}
