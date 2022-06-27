namespace MyUtils
{
    /// <summary>
    /// Класс, описывающий свойства и методы колонки в таблице
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Ссылка на родительскую таблицу
        /// </summary>
        private Table Owner;

        public Column(Table Owner, int Width) {
            this.Owner = Owner;
            this.Width = Width;
        }
    }
}
