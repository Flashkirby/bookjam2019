using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Config
{
    public const float INTERACT_COOLDOWN = 0.5f;
    public const float INTERACT_IDENTIFY_DELAY = 1f;

    public const float MAX_WALK_SPEED = 5f;
    public const float MAX_EMPLOYEE_WALK_SPEED = 1f;

    public const int PIXEL_PER_UNIT = 16;
    public const int PIXEL_PER_UNIT_ZOOM_OUT = 8;
    public const float PIXEL = 1f / 16; // 0.0625f

    public const float CONTROL_DEADZONE = 0.3f;

    public const float CONTROL_MIN_HOLD = 0.25f;
    public const float CONTROL_MAX_HOLD = 1.5f;

    public const float FLOOR_HEIGHT = 6;

    public const float LIFT_SPEED = 2f;

    public const float MINGLE_WAIT_MIN = 2f;
    public const float MINGLE_WAIT_MAX = 10f;
    public const float MINGLE_WALK_MIN = 2f;
    public const float MINGLE_WALK_MAX = 5f;
    public const float MINGLE_WALK_BUMP_TIME = 0.25f;

    public const float ANXIETY_DIALOGUE_INCREASE = 0.05f;
    public const float ANXIETY_DIALOGUE_WITH_TARGET_INCREASE = 0.30f;
    public const float ANXIETY_WRONG_IDENTIFY_INCREASE = 0.30f;

    public const int SORT_ORDER_FURNITURE = -1;
}
