namespace Lesson5
{
    interface InterfaceTask
    {
        /// <summary>
        /// Короткое описание задачи (для списка задач)
        /// </summary>
        string ShortDescription { get; }

        /// <summary>
        /// Основной метод задачи.
        /// </summary>
        void Execute();
    }
}
