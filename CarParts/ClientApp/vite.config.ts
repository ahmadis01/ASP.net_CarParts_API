import { fileURLToPath, URL } from 'url'
import postcssRtl from 'postcss-rtlcss';
import tailwidCss from 'tailwindcss'
import * as path from 'path'
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { url } from 'inspector';
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()

  ],
  resolve: {
    alias: [{ find: '~', replacement: path.resolve(__dirname, 'src') }, { find: '@', replacement: path.resolve(__dirname, 'src') }],

  },
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
