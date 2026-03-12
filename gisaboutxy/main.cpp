#include "gisaboutxy.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    gisaboutxy w;
    w.show();
    return a.exec();
}
