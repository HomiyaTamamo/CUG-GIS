/********************************************************************************
** Form generated from reading UI file 'gisaboutxy.ui'
**
** Created by: Qt User Interface Compiler version 5.11.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_GISABOUTXY_H
#define UI_GISABOUTXY_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_gisaboutxyClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *gisaboutxyClass)
    {
        if (gisaboutxyClass->objectName().isEmpty())
            gisaboutxyClass->setObjectName(QStringLiteral("gisaboutxyClass"));
        gisaboutxyClass->resize(600, 400);
        menuBar = new QMenuBar(gisaboutxyClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        gisaboutxyClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(gisaboutxyClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        gisaboutxyClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(gisaboutxyClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gisaboutxyClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(gisaboutxyClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        gisaboutxyClass->setStatusBar(statusBar);

        retranslateUi(gisaboutxyClass);

        QMetaObject::connectSlotsByName(gisaboutxyClass);
    } // setupUi

    void retranslateUi(QMainWindow *gisaboutxyClass)
    {
        gisaboutxyClass->setWindowTitle(QApplication::translate("gisaboutxyClass", "gisaboutxy", nullptr));
    } // retranslateUi

};

namespace Ui {
    class gisaboutxyClass: public Ui_gisaboutxyClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_GISABOUTXY_H
