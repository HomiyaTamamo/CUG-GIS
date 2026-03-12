#ifndef DECISIONTREE_H
#define DECISIONTREE_H

#include <vector>
#include <map>
#include <string>
#include <set>

// 数据节点
class Data {
public:
    std::map<std::string, std::string> attributes;
    std::string label;
};

// 定义一个结构来存储增益和每个特征值的权重
class FeatureWeight {
public:
    double gain;  // 信息增益
    std::map<std::string, double> values;  // 每个取值的权重
};

// 决策树节点
class TreeNode {
public:
    std::string attribute;                // 节点的属性
    std::string weight;                   // 节点的权重
    std::map<std::string, TreeNode*> children;  // 子节点（按属性值划分）
    std::string label;                    // 叶子节点的标签
    bool isLeaf;                          // 是否是叶子节点
    TreeNode* left;                       // 左子节点（用于绘制树）
    TreeNode* right;                      // 右子节点（用于绘制树）

    // 构造函数初始化成员变量
    TreeNode() : isLeaf(false), left(nullptr), right(nullptr) {}

    // 析构函数，递归删除所有子节点
    ~TreeNode() {
        for (auto& child : children) {
            delete child.second;  // 递归删除所有子节点
        }
    }

    // 成员函数：添加子节点
    void addChild(const std::string& key, TreeNode* child) {
        children[key] = child;
    }

    // 成员函数：设置节点标签
    void setLabel(const std::string& nodeLabel) {
        label = nodeLabel;
    }

    // 成员函数：设置节点是否为叶子节点
    void setLeaf(bool leafStatus) {
        isLeaf = leafStatus;
    }

    // 成员函数：设置节点的属性
    void setAttribute(const std::string& nodeAttribute) {
        attribute = nodeAttribute;
    }
};

std::vector<Data> loadData(const std::string& filename);
double entropy(const std::vector<Data>& dataset);
double informationGain(const std::vector<Data>& dataset, const std::string& attribute);
TreeNode* buildTree(std::vector<Data> dataset, std::set<std::string> usedAttributes);
std::string predict(TreeNode* root, const std::map<std::string, std::string>& testAttributes);
std::map<std::string, double> computeFeatureWeightsSimple(const std::vector<Data>& dataset);
std::map<std::string, FeatureWeight> computeFeatureWeightsDetailed(const std::vector<Data>& dataset);

#endif // DECISIONTREE_H
