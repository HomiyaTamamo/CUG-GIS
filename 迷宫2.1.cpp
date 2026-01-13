#include<iostream>
#include<ctime>
#include<windows.h>
#include<stdlib.h>
#include<fstream>
#include<stdio.h>
using namespace std;

//常量定义
const int N = 100;
char back[N][N] = { 0 };
const char wall = '#';
const char path = ' ';
const char player = 'P';
const char enemy = 'E';
const char treasure = 'T';
const char sign = '$';
int score = 0;
const int dir[4][2] =//方向数组，表示前进方向
{
	{-1,0},//向上移动
	{1,0},//向下移动
	{0,-1},//向左移动
	{0,1}//向右移动
};
int x;
int y;//定义玩家坐标

//函数声明
void generatemap(int count, int enemy_num, int& enemy_x, int& enemy_y);//初始化迷宫地图
void displaymap(int count, int& enemy_x, int& enemy_y);//打印迷宫地图
void create(int x, int y, int count);//打通所有的节点
int inarea(int x, int y, int count);//判断位置是否合法
int havepath(int x, int y, int count);//判断是否在周围存在未被标记的节点
void enemy_produce(int enemy_num, int count, int& enemy_x, int& enemy_y);//生成敌人
void move_system(int& x, int& y, char direction);//控制任务与敌人的移动（敌人会朝靠近玩家的方向移动）
int otherpath(int x, int y);//判断当前位置周围是否有其他为被标记的道路
void SetColorAndBackground(int ForgC, int BackC);//
void enemy_move_system(int x, int y, int count, char(*back)[100]);
void game_save(char(*back)[100], int count);//保存游戏
int game_load();//读取游戏
int get_x(int count);//获取当前纵坐标
int get_y(int count);//获取当前横坐标
void game_run(int count, int enemy_x, int enemy_y);//游戏运行函数
void Score_Record(int dx, int dy, int& score);//计分

int main()
{
	int choose;
	int enemy_x, enemy_y;
	srand((unsigned int)time(NULL));//根据当前时间产生随机数种子
	/*cout << "请选择您的游戏：" << endl
		<< "1.新游戏" << endl
		<< "2.继续上次游戏" << endl;*/

	SetColorAndBackground(6, 0);
	cout << "@============================================================@" << endl; Sleep(10);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(4, 0);
	cout << "||                     【迷 宫 游 戏】                      ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(6, 0);
	cout << "||                     请选择您的游戏                       ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(6, 0);
	cout << "||                     按‘1’开始新游戏                    ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(6, 0);
	cout << "||                    按‘2’继续上次游戏                   ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(6, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(15, 0);
	cout << "||                                                          ||" << endl; Sleep(10);
	SetColorAndBackground(6, 0);
	cout << "||                   （按‘3’看玩法说明)                   ||" << endl; Sleep(10);
	cout << "@============================================================@" << endl;

	cin >> choose;
	system("cls");
	switch (choose)
	{
		//新游戏
	case 1:
	INPUT:int difficulty, count, enemy_num;
		/*cout << "请选择你的难度：" << endl
			<< "1.简单" << endl
			<< "2.普通" << endl
			<< "3.困难" << endl;*/

		SetColorAndBackground(6, 0);
		cout << "@============================================================@" << endl; Sleep(10);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(4, 0);
		cout << "||                     【迷 宫 游 戏】                      ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(6, 0);
		cout << "||                       按‘1’选择简单                    ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(6, 0);
		cout << "||                       按‘2’选择普通                    ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(6, 0);
		cout << "||                       按‘3’选择困难                    ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(6, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(15, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		SetColorAndBackground(6, 0);
		cout << "||                                                          ||" << endl; Sleep(10);
		cout << "@============================================================@" << endl;


		cin >> difficulty;
		switch (difficulty)
		{
		case 1:
			count = 5, enemy_num = 0;
			break;
		case 2:
			count = 27, enemy_num = 2;
			break;
		case 3:
			count = 35, enemy_num = 3;
			break;
		default:
			cout << "未选择难度" << endl;
			system("cls");
			goto INPUT;
		}
		system("cls");
		generatemap(count, enemy_num, enemy_x, enemy_y);//初始化迷宫
		char direction;
		x = y = 1;
		for (; back[x][y] != treasure;)
		{
			int dx, dy;//坐标的变化量
			dx = dy = 0;
			displaymap(count, enemy_x, enemy_y);//打印迷宫
			cin >> direction;
			move_system(dx, dy, direction);
			//游戏规则的判定
			if (back[x + dx][y + dy] == wall)
			{
				system("cls");
				cout << "不可移动的方向,请重新输入！！" << endl;
			}
			else if (back[x + dx][y + dy] == enemy)
			{
				system("cls");
				cout << "遭遇敌人，你失败了！" << endl;
			}
			else if (back[x + dx][y + dy] == treasure)
			{
				system("cls");
				Score_Record(dx, dy, score);
			}
			else
			{
				back[x + dx][y + dy] = player;
				back[x][y] = path;
				x = x + dx;//新位置的玩家坐标
				y = y + dy;
				enemy_move_system(x, y, count, back);//敌人的移动
				system("cls");
				displaymap(count, enemy_x, enemy_y);
				game_save(back, count);
				system("cls");
				Score_Record(dx, dy, score);
			}
		}break;
		//继续上一次的游戏
	case 2:
		//读取上一次游戏的存档
		game_load();
		int count2;
		count2 = game_load();
		x = get_x(count2);
		y = get_y(count2);
		for (; back[x][y] != treasure;)
		{
			int dx, dy;//坐标的变化量
			dx = dy = 0;
			displaymap(count2, enemy_x, enemy_y);//打印迷宫
			cin >> direction;
			cout << direction;
			move_system(dx, dy, direction);
			//游戏规则的判定
			if (back[x + dx][y + dy] == wall)
			{
				system("cls");
				cout << "不可移动的方向,请重新输入！！" << endl;
			}
			else if (back[x + dx][y + dy] == enemy)
			{
				system("cls");
				cout << "遭遇敌人，你失败了！" << endl;
			}
			else
			{
				back[x + dx][y + dy] = player;
				back[x][y] = path;
				x = x + dx;//新位置的玩家坐标
				y = y + dy;
				enemy_move_system(x, y, count2, back);//敌人的移动
				system("cls");
				displaymap(count2, enemy_x, enemy_y);
				game_save(back, count2);
				system("cls");
			}
		}
	}
}

int inarea(int x, int y, int count)
{
	int isoutside = 0;
	if (x < (count - 1) && y < (count - 1))
	{
		isoutside = 1;
	}
	return isoutside;
}
void generatemap(int count, int enemy_num, int& enemy_x, int& enemy_y)
{
	x = y = 1;
	int i, j;
	for (i = 0; i < count; ++i)
	{
		for (j = 0; j < count; ++j)
		{
			back[i][j] = wall;
		}
	}
	//生成需要的节点
	for (i = 1; i < (count - 1); ++i)
	{
		for (j = 1; j < (count - 1); ++j)
		{
			if (i % 2 != 0 && j % 2 == 1)
			{
				back[i][j] = path;
			}
		}
	}
	//生成迷宫
	create(x, y, count);
	//将各个节点进行替换
	for (i = 0; i < count; i++)
	{
		for (j = 0; j < count; j++)
		{
			if (back[i][j] == sign)
			{
				back[i][j] = path;
			}
		}
	}
	enemy_produce(enemy_num, count, enemy_x, enemy_y);//生成敌人
	back[1][1] = player;//玩家位置
	back[count - 2][count - 2] = treasure;//生成宝藏位置
}
//生成迷宫
void create(int x, int y, int count)
{
	int i;
	while (otherpath(x, y))
	{
		/*srand((unsigned int)time(NULL));*/
		i = rand() % 4;
		if (inarea(x + 2 * dir[i][0], y + 2 * dir[i][1], count) && back[x + 2 * dir[i][0]][y + 2 * dir[i][1]] == path)
		{
			back[x + dir[i][0]][y + dir[i][1]] = sign;
			back[x + 2 * dir[i][0]][y + 2 * dir[i][1]] = sign;
			create(x + 2 * dir[i][0], y + 2 * dir[i][1], count);//将新的位置进行赋值，此时x=x + 2 * dir[i][0]
		}
	}
}

//SetColorAndBackground(4,0);////左字右底-红
//SetColorAndBackground(15,0);////左字右底-白
//SetColorAndBackground(2,0);////左字右底-绿
//SetColorAndBackground(6,0);////左字右底-黄


//打印迷宫
void displaymap(int count, int& enemy_x, int& enemy_y)
{
	for (int i = 0; i < count; i++)
	{
		for (int j = 0; j < count; j++)
		{
			if (j == count - 1)
			{
				cout << back[i][j] << endl;
			}
			else {
				if (i == enemy_x, j == enemy_y)
				{
					//SetColorAndBackground(4, 0);
					cout << back[i][j];
					//SetColorAndBackground(15, 0);
				}
				else {
					cout << back[i][j];
				}
			}
		}
	}
}
//判断所处的位置是否合法
int havepath(int x, int y, int count)
{
	int isoutside = 0;
	if (x < (count - 1) && y < (count - 1))
	{
		isoutside = 1;
	}
	return isoutside;
}
//判断当前位置的周围有无未被标记的道路
int otherpath(int x, int y)
{
	int nonepath = 0;
	for (int i = 0; i < 4; i++)
	{
		if (back[x + 2 * dir[i][0]][y + 2 * dir[i][1]] == path)
		{
			nonepath = 1;
			break;
		}
	}
	return nonepath;
}
//生成敌人
void enemy_produce(int enemy_num, int count, int& enemy_x, int& enemy_y)
{

	for (int i = 0; i <= enemy_num; i++)
	{
		enemy_x = rand() % ((count / 2) - (count - 2)) + (count / 2) + i;
		enemy_y = rand() % ((count / 2) - (count - 2)) + (count / 2);
		if (back[enemy_x][enemy_y] != (wall && treasure))
		{
			back[enemy_x][enemy_y] = enemy;
		}
	}
}
//更改颜色
void SetColorAndBackground(int ForgC, int BackC) {
	WORD wColor = ((BackC & 0x0F) << 4) + (ForgC & 0x0F);
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), wColor);
}

//移动系统
void move_system(int& x, int& y, char direction)
{
	switch (direction) {
	case 'A':
		y--;
		break;
	case 'D':
		y++;
		break;
	case 'W':
		x--;
		break;
	case 'S':
		x++;
		break;
	case 'Q':
		x--, y--;
		break;
	case 'E':
		x--, y++;
		break;
	case 'Z':
		x++, y--;
		break;
	case 'C':
		x++, y++;
	case 'X':
		x--, x++, y--, y++;
		break;
		/*case 'Z':
			for (int i = -1; i <= 1; i++) {
				for (int j = -1; j <= 1; j++) {
					int adjRow = newRow + i;
					int adjCol = newCol + j;
					if (adjRow >= 0 && adjRow < numRows && adjCol >= 0 && adjCol < numCols
						&& maze[adjRow][adjCol] == '#') {
						maze[adjRow][adjCol] = ' ';
					}
				}
			}
			break;*/
	default:
		cout << "错误的方向" << endl;
		return;
	}
}
//敌人移动逻辑
void enemy_move_system(int x, int y, int count, char(*back)[100])
{
	int num_enemy = 0 /*get_x, get_y*/;//定义敌人的位置，获取玩家的位置，位置的移动量
	int sit[10][2];//储存所有敌人的当前位置
	for (int i = 0; i < count - 1; i++)
	{
		for (int j = 1; j < count; j++)
		{
			if (back[i][j] == enemy) {
				++num_enemy;
				sit[num_enemy - 1][0] = i;
				sit[num_enemy - 1][1] = j;
			}
		}
	}
	for (int m = 1; m <= num_enemy; m++)
	{
		//// 移动敌人
		//int n = rand() % 40;
		//int direction = n%4;
		//if (back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] != wall 
		//	&& back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] != treasure)
		//{
		//	back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] = enemy;
		//	back[sit[m - 1][0]][sit[m - 1][1]] = path;
		//}
		int space1 = abs(sit[m - 1][0] - x);//敌人与玩家的纵坐标差值
		int space2 = abs(sit[m - 1][1] - y);//敌人与玩家的横坐标差值
		//根据玩家的位置移动
		if (space1 > space2)//判断两者的差值哪个大，并往较近的方向移动
		{
			if (sit[m - 1][0] > x && back[sit[m - 1][0] + dir[0][0]][sit[m - 1][1] + dir[0][1]] != wall//若玩家在敌人上方，则向上移动
				&& back[sit[m - 1][0] + dir[0][0]][sit[m - 1][1] + dir[0][1]] != treasure) {
				back[sit[m - 1][0] + dir[0][0]][sit[m - 1][1] + dir[0][1]] = enemy;
				back[sit[m - 1][0]][sit[m - 1][1]] = path;
				continue;
			}
			else if ((sit[m - 1][0] < x || sit[m - 1][0] > x) && back[sit[m - 1][0] + dir[1][0]][sit[m - 1][1] + dir[1][1]] != wall//若玩家在敌人下方，则向下移动
				&& back[sit[m - 1][0] + dir[1][0]][sit[m - 1][1] + dir[1][1]] != treasure) {
				back[sit[m - 1][0] + dir[1][0]][sit[m - 1][1] + dir[1][1]] = enemy;
				back[sit[m - 1][0]][sit[m - 1][1]] = path;
			}
		}
		else if (space1 < space2) {
			if (sit[m - 1][1] > y && back[sit[m - 1][0] + dir[2][0]][sit[m - 1][1] + dir[2][1]] != wall//若玩家在敌人左边，则向左移动
				&& back[sit[m - 1][0] + dir[2][0]][sit[m - 1][1] + dir[2][1]] != treasure) {
				back[sit[m - 1][0] + dir[2][0]][sit[m - 1][1] + dir[2][1]] = enemy;
				back[sit[m - 1][0]][sit[m - 1][1]] = path;
				continue;
			}
			else if ((sit[m - 1][1] < y || sit[m - 1][1] > y) && back[sit[m - 1][0] + dir[3][0]][sit[m - 1][1] + dir[3][1]] != wall//若玩家在敌人右边，则向右移动
				&& back[sit[m - 1][0] + dir[3][0]][sit[m - 1][1] + dir[3][1]] != treasure) {
				back[sit[m - 1][0] + dir[3][0]][sit[m - 1][1] + dir[3][1]] = enemy;
				back[sit[m - 1][0]][sit[m - 1][1]] = path;
			}
		}
		else {//若以上情况都不满足，则随机一个方向移动
			int n = rand() % 40;
			int direction = n % 4;
			if (back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] != wall
				&& back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] != treasure)
			{
				back[sit[m - 1][0] + dir[direction][0]][sit[m - 1][1] + dir[direction][1]] = enemy;
				back[sit[m - 1][0]][sit[m - 1][1]] = path;
			}
		}
	}
}

//根据玩家的方向来进行移动
//			get_x = x; // 获取玩家的行坐标
//			get_y = y; // 获取玩家的列坐标

//			if (back[enemy_x - 1][enemy_y] != '#' && enemy_x < get_x && back[enemy_x-1][enemy_y]!=treasure) { // 如果上方不是墙壁 并且玩家在敌人上方
//				back[enemy_x + dir[0][0]][enemy_y + dir[0][1]] = enemy;
//				back[enemy_x][enemy_y] = path;//向上移动
//			}
//			else if (back[enemy_x + 1][enemy_y] != '#' && enemy_x > get_x && back[enemy_x + 1][enemy_y] != treasure) { // 如果下方不是墙壁 并且玩家在敌人下方
//				back[enemy_x + dir[1][0]][enemy_y + dir[1][1]] = enemy;
//				back[enemy_x][enemy_y] = path;//向上移动
//			}
//			else if (back[enemy_x][enemy_y-1] != '#' && enemy_y < get_y && back[enemy_x][enemy_y-1] != treasure) { // 如果左边不是墙壁 并且 玩家在敌人左边
//				back[enemy_x + dir[2][0]][enemy_y + dir[2][1]] = enemy;
//				back[enemy_x][enemy_y] = path; // 向左移动
//			}
//			else if (back[enemy_x + 1][enemy_y] != '#' && enemy_y > get_y && back[enemy_x + 1][enemy_y] != treasure) { // 如果右边不是墙壁 并且 玩家在敌人右边
//				back[enemy_x + dir[3][0]][enemy_y + dir[3][1]] = enemy;
//				back[enemy_x][enemy_y] = path; // 向右移动
//			}
//		}
//	}
//}
void game_save(char(*back)[100], int count)
{
	ofstream ofs;
	ofs.open("C:\\Users\\27378\\Desktop\\迷宫\\game_data.txt", ios::trunc);
	if (!ofs.is_open())
	{
		cout << "文件打开失败，请检查文件是否存在" << endl;
	}
	char save[N][N] = { 0 };
	for (int i = 0; i < count; i++)
	{
		for (int j = 0; j < count; j++)
		{
			save[i][j] = back[i][j];
		}
	}
	for (int m = 0; m < count; m++)
	{
		ofs << save[m] << endl;
	}
	ofs.close();
}
int game_load()
{
	ifstream ifs;
	ifs.open("D:\\HomC++\\迷宫2.1\\game_data.txt", ios::in);
	if (!ifs.is_open())
	{
		cout << "暂无存档文件，请开始你的游戏！" << endl;
	}
	char line[100] = { 0 };
	ifs.getline(line, 100);
	int i = 0;
	for (; line[i] != '\0'; )
	{
		i++;
	}
	char c;
	int n = 0;
	while ((c = ifs.get()) != EOF)
	{

	}
	ifs.close();
	return i;
}
//获取当前的纵坐标
int get_x(int count)
{
	int x;
	for (int i = 0; i < count; i++)
	{
		for (int j = 0; j < count; j++)
		{
			if (back[i][j] == player)
			{
				x = i;
				break;
			}
		}
	}
	return x;
}
//获取当前的横坐标
int get_y(int count)
{
	int y;
	for (int i = 0; i < count; i++)
	{
		for (int j = 0; j < count; j++)
		{
			if (back[i][j] == player)
			{
				y = j;
				break;
			}
		}
	}
	return y;
}
//游戏运行
void game_run(int count, int enemy_x, int enemy_y)
{
	char direction;
	x = y = 1;
	for (; back[x][y] != treasure;)
	{
		int dx, dy;//坐标的变化量
		dx = dy = 0;
		displaymap(count, enemy_x, enemy_y);//打印迷宫
		cin >> direction;
		move_system(dx, dy, direction);
		//游戏规则的判定
		if (back[x + dx][y + dy] == wall)
		{
			system("cls");
			cout << "不可移动的方向,请重新输入！！" << endl;
		}
		else if (back[x + dx][y + dy] == enemy)
		{
			system("cls");
			cout << "遭遇敌人，你失败了！" << endl;
		}
		else
		{
			back[x + dx][y + dy] = player;
			back[x][y] = path;
			x = x + dx;//新位置的玩家坐标
			y = y + dy;
			enemy_move_system(x, y, count, back);//敌人的移动
			system("cls");
			displaymap(count, enemy_x, enemy_y);
			game_save(back, count);
			system("cls");
		}
	}
}
//记录分数，并输出得分
void Score_Record(int dx, int dy, int& score) {
	if (back[x + dx][y + dy] == treasure) {
		score++;
		cout << "您当前的分数是：" << score << endl;
	}
	else {
		cout << "您当前的分数是：" << score << endl;
	}
}
