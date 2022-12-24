import { fileURLToPath, URL } from 'url'
import postcssRtl  from 'postcss-rtlcss';
import tailwidCss from 'tailwindcss'

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()

  ],

  css: {
    postcss: {
      plugins: [
        postcssRtl(),
        tailwidCss({
          config: './tailwind.config.cjs'
        })
      ]
    }
  },


})
