/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],


  theme: {

    extend: {
      colors: {
        primary: {
          100: "#d7dffb",
          200: "#afc0f6",
          300: "#87a0f2",
          400: "#5f81ed",
          500: "#3761e9",
          600: "#2c4eba",
          700: "#213a8c",
          800: "#16275d",
          900: "#0b132f",
          DEFAULT: "#3761E9"
          
        },
      }
    },

  },
  plugins: [],
}