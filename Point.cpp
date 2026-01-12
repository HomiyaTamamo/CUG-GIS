#include "Point.h"
#include <QPainter>
#include <QFont>

void Point::draw(QPainter& painter, const QPointF& center) const {
    // 使用较小的圆形标记显示点，修改为小尺寸
    painter.setBrush(Qt::blue);
    painter.drawEllipse(center, 3, 3);  // 使用较小的半径
}

QString Point::getDisplayName() const {
    if (name.length() > 10) {
        return name.left(10) + "...";  // 截取前10个字符
    }
    return name;
}

void Point::drawName(QPainter& painter, const QPointF& center) const {
    painter.setPen(Qt::black);
    painter.setFont(QFont("Arial", 10));
    QString displayName = getDisplayName();
    painter.drawText(center + QPointF(5, -10), displayName);  // 显示名称
}
