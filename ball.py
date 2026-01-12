import pygame
import math

# 初始化Pygame
pygame.init()

# 窗口设置
WIDTH, HEIGHT = 800, 600
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("旋转方块内的弹跳球")

# 颜色常量
WHITE = (255, 255, 255)
RED = (255, 0, 0)
BLACK = (0, 0, 0)

# 物理参数
CENTER = (WIDTH // 2, HEIGHT // 2)
SQUARE_SIZE = 400
BALL_RADIUS = 10
GRAVITY = 800  # 像素/秒²
BOUNCE_COEFF = 0.7  # 反弹系数
FRICTION = 0.98  # 空气阻力系数
ANGULAR_SPEED = math.radians(45)  # 旋转速度（弧度/秒）

# 初始化球的状态
ball_pos = [CENTER[0], CENTER[1] - 100]
ball_vel = [50, 0]  # 初始速度
theta = 0  # 方块旋转角度

clock = pygame.time.Clock()


def rotate_point(point, angle, center):
    """将点绕中心旋转指定角度"""
    x, y = point
    cx, cy = center
    x_trans = x - cx
    y_trans = y - cy
    cos_theta = math.cos(angle)
    sin_theta = math.sin(angle)
    x_new = x_trans * cos_theta - y_trans * sin_theta
    y_new = x_trans * sin_theta + y_trans * cos_theta
    return (x_new + cx, y_new + cy)


running = True
while running:
    dt = clock.tick(60) / 1000.0  # 获取时间差（秒）

    # 处理退出事件
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False

    # 更新方块旋转角度
    theta += ANGULAR_SPEED * dt
    theta %= 2 * math.pi

    # 将球的坐标转换到局部坐标系
    x_trans = ball_pos[0] - CENTER[0]
    y_trans = ball_pos[1] - CENTER[1]
    cos_theta = math.cos(theta)
    sin_theta = math.sin(theta)
    local_x = x_trans * cos_theta + y_trans * sin_theta
    local_y = -x_trans * sin_theta + y_trans * cos_theta

    # 碰撞检测与响应
    half_size = SQUARE_SIZE // 2
    collision = False
    new_local = (local_x, local_y)
    normal = (0, 0)

    # 检测各边碰撞
    if local_x + BALL_RADIUS > half_size:  # 右边碰撞
        new_local = (half_size - BALL_RADIUS, local_y)
        normal = (-cos_theta, -sin_theta)
        collision = True
    elif local_x - BALL_RADIUS < -half_size:  # 左边碰撞
        new_local = (-half_size + BALL_RADIUS, local_y)
        normal = (cos_theta, sin_theta)
        collision = True

    if local_y + BALL_RADIUS > half_size:  # 下边碰撞
        new_local = (local_x, half_size - BALL_RADIUS)
        normal = (sin_theta, -cos_theta)
        collision = True
    elif local_y - BALL_RADIUS < -half_size:  # 上边碰撞
        new_local = (local_x, -half_size + BALL_RADIUS)
        normal = (-sin_theta, cos_theta)
        collision = True

    if collision:
        # 速度反射计算
        norm_length = math.hypot(*normal)
        if norm_length > 0:
            nx, ny = normal[0] / norm_length, normal[1] / norm_length
            dot_product = ball_vel[0] * nx + ball_vel[1] * ny
            ball_vel[0] = BOUNCE_COEFF * (ball_vel[0] - 2 * dot_product * nx)
            ball_vel[1] = BOUNCE_COEFF * (ball_vel[1] - 2 * dot_product * ny)

        # 位置修正
        x_rot = new_local[0] * cos_theta - new_local[1] * sin_theta
        y_rot = new_local[0] * sin_theta + new_local[1] * cos_theta
        ball_pos = [x_rot + CENTER[0], y_rot + CENTER[1]]

    # 应用物理效果
    ball_vel[1] += GRAVITY * dt  # 重力
    ball_vel[0] *= FRICTION  # 空气阻力
    ball_vel[1] *= FRICTION

    if not collision:  # 只在无碰撞时更新位置
        ball_pos[0] += ball_vel[0] * dt
        ball_pos[1] += ball_vel[1] * dt

    # 绘制场景
    screen.fill(BLACK)

    # 绘制旋转方块
    points = [
        (CENTER[0] - SQUARE_SIZE // 2, CENTER[1] - SQUARE_SIZE // 2),
        (CENTER[0] + SQUARE_SIZE // 2, CENTER[1] - SQUARE_SIZE // 2),
        (CENTER[0] + SQUARE_SIZE // 2, CENTER[1] + SQUARE_SIZE // 2),
        (CENTER[0] - SQUARE_SIZE // 2, CENTER[1] + SQUARE_SIZE // 2)
    ]
    rotated_points = [rotate_point(p, theta, CENTER) for p in points]
    pygame.draw.lines(screen, WHITE, True, rotated_points, 2)

    # 绘制球
    pygame.draw.circle(screen, RED, (int(ball_pos[0]), int(ball_pos[1])), BALL_RADIUS)

    pygame.display.flip()

pygame.quit()
