using Balance;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static SlotType ToSlotType(this int index)
            => (SlotType) index;

        public static int ToSlotIndex(this SlotType type)
            => (int) type;
    }
}