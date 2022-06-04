using System;

namespace Lesson6
{
    /// <summary>
    /// Класс для описания цвета текста и фона
    /// </summary>
    class TextColors
    {
        /// <summary>
        /// Цвет фона
        /// </summary>
        public ConsoleColor Background { get; set; }

        /// <summary>
        /// Цвет текста
        /// </summary>
        public ConsoleColor Foreground { get; set; }
        
        public TextColors() {
            Background = ConsoleColor.Black;
            Foreground = ConsoleColor.White;
        }

        
        public TextColors(ConsoleColor Foreground, ConsoleColor Background)
        {
            this.Foreground = Foreground;
            this.Background = Background;
        }

        /// <summary>
        /// Применение цветовых настроек
        /// </summary>
        public void Apply() {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Foreground;
        }
    }
}
