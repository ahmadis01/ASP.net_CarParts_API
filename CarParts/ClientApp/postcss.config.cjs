const rtlcss = require('postcss-rtlcss');
module.exports = {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
    postcssRtl: rtlcss()
  },
}
