# treePlotter.py
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
import tkinter as tk
import matplotlib

# 设置中文字体，避免中文乱码
matplotlib.rcParams['font.sans-serif'] = ['SimHei']  # 中文黑体
matplotlib.rcParams['axes.unicode_minus'] = False    # 解决负号显示问题

decisionNode = dict(boxstyle="sawtooth", fc="0.8")
leafNode = dict(boxstyle="round4", fc="0.8")
arrow_args = dict(arrowstyle="<-")

def getNumLeafs(myTree):
    numLeafs = 0
    firstStr = list(myTree.keys())[0]
    secondDict = myTree[firstStr]
    for key in secondDict.keys():
        if isinstance(secondDict[key], dict):
            numLeafs += getNumLeafs(secondDict[key])
        else:
            numLeafs += 1
    return numLeafs

def getTreeDepth(myTree):
    maxDepth = 0
    firstStr = list(myTree.keys())[0]
    secondDict = myTree[firstStr]
    for key in secondDict.keys():
        if isinstance(secondDict[key], dict):
            thisDepth = 1 + getTreeDepth(secondDict[key])
        else:
            thisDepth = 1
        maxDepth = max(maxDepth, thisDepth)
    return maxDepth

def plotNode(nodeTxt, centerPt, parentPt, nodeType, ax):
    ax.annotate(nodeTxt, xy=parentPt,  xycoords='axes fraction',
                xytext=centerPt, textcoords='axes fraction',
                va="center", ha="center", bbox=nodeType, arrowprops=arrow_args, fontsize=9)

def plotMidText(cntrPt, parentPt, txtString, ax):
    xMid = (parentPt[0] - cntrPt[0]) / 2.0 + cntrPt[0]
    yMid = (parentPt[1] - cntrPt[1]) / 2.0 + cntrPt[1]
    ax.text(xMid, yMid, txtString, fontsize=8)

def plotTree(myTree, parentPt, nodeTxt, ax):
    numLeafs = getNumLeafs(myTree)
    firstStr = list(myTree.keys())[0]
    cntrPt = (plotTree.xOff + (1.0 + float(numLeafs)) / 2.0 / plotTree.totalW, plotTree.yOff)
    plotMidText(cntrPt, parentPt, nodeTxt, ax)
    plotNode(firstStr, cntrPt, parentPt, decisionNode, ax)
    secondDict = myTree[firstStr]
    plotTree.yOff = plotTree.yOff - 1.0 / plotTree.totalD
    for key in secondDict.keys():
        if isinstance(secondDict[key], dict):
            plotTree(secondDict[key], cntrPt, str(key), ax)
        else:
            plotTree.xOff = plotTree.xOff + 1.0 / plotTree.totalW
            plotNode(secondDict[key], (plotTree.xOff, plotTree.yOff), cntrPt, leafNode, ax)
            plotMidText((plotTree.xOff, plotTree.yOff), cntrPt, str(key), ax)
    plotTree.yOff = plotTree.yOff + 1.0 / plotTree.totalD

def drawTree(tree, ax, title='决策树'):
    plotTree.totalW = float(getNumLeafs(tree))
    plotTree.totalD = float(getTreeDepth(tree))
    plotTree.xOff = -0.5 / plotTree.totalW
    plotTree.yOff = 1.0
    ax.set_title(title, fontsize=12)
    plotTree(tree, (0.5, 1.0), '', ax)
    ax.set_xticks([])
    ax.set_yticks([])

def showAllTrees(ID3tree, C45tree, CARTtree):
    # tkinter 主窗口
    win = tk.Tk()
    win.title("三种决策树可视化")

    fig, axs = plt.subplots(1, 3, figsize=(15, 6), facecolor='white')
    drawTree(ID3tree, axs[0], title='ID3 决策树')
    drawTree(C45tree, axs[1], title='C4.5 决策树')
    drawTree(CARTtree, axs[2], title='CART 决策树')

    canvas = FigureCanvasTkAgg(fig, master=win)
    canvas.draw()
    canvas.get_tk_widget().pack(fill=tk.BOTH, expand=True)

    win.mainloop()
