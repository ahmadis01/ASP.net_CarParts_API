// vite.config.ts
import postcssRtl from "file:///C:/Users/ASUS/Desktop/Workspace/car-parts-v3/node_modules/postcss-rtlcss/esm/index.js";
import tailwidCss from "file:///C:/Users/ASUS/Desktop/Workspace/car-parts-v3/node_modules/tailwindcss/lib/index.js";
import { defineConfig } from "file:///C:/Users/ASUS/Desktop/Workspace/car-parts-v3/node_modules/vite/dist/node/index.js";
import react from "file:///C:/Users/ASUS/Desktop/Workspace/car-parts-v3/node_modules/@vitejs/plugin-react/dist/index.mjs";
var vite_config_default = defineConfig({
  plugins: [
    react()
  ],
  css: {
    postcss: {
      plugins: [
        postcssRtl(),
        tailwidCss({
          config: "./tailwind.config.cjs"
        })
      ]
    }
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcudHMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCJDOlxcXFxVc2Vyc1xcXFxBU1VTXFxcXERlc2t0b3BcXFxcV29ya3NwYWNlXFxcXGNhci1wYXJ0cy12M1wiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9maWxlbmFtZSA9IFwiQzpcXFxcVXNlcnNcXFxcQVNVU1xcXFxEZXNrdG9wXFxcXFdvcmtzcGFjZVxcXFxjYXItcGFydHMtdjNcXFxcdml0ZS5jb25maWcudHNcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfaW1wb3J0X21ldGFfdXJsID0gXCJmaWxlOi8vL0M6L1VzZXJzL0FTVVMvRGVza3RvcC9Xb3Jrc3BhY2UvY2FyLXBhcnRzLXYzL3ZpdGUuY29uZmlnLnRzXCI7aW1wb3J0IHsgZmlsZVVSTFRvUGF0aCwgVVJMIH0gZnJvbSAndXJsJ1xuaW1wb3J0IHBvc3Rjc3NSdGwgIGZyb20gJ3Bvc3Rjc3MtcnRsY3NzJztcbmltcG9ydCB0YWlsd2lkQ3NzIGZyb20gJ3RhaWx3aW5kY3NzJ1xuXG5pbXBvcnQgeyBkZWZpbmVDb25maWcgfSBmcm9tICd2aXRlJ1xuaW1wb3J0IHJlYWN0IGZyb20gJ0B2aXRlanMvcGx1Z2luLXJlYWN0J1xuLy8gaHR0cHM6Ly92aXRlanMuZGV2L2NvbmZpZy9cbmV4cG9ydCBkZWZhdWx0IGRlZmluZUNvbmZpZyh7XG4gIHBsdWdpbnM6IFtyZWFjdCgpXG5cbiAgXSxcblxuICBjc3M6IHtcbiAgICBwb3N0Y3NzOiB7XG4gICAgICBwbHVnaW5zOiBbXG4gICAgICAgIHBvc3Rjc3NSdGwoKSxcbiAgICAgICAgdGFpbHdpZENzcyh7XG4gICAgICAgICAgY29uZmlnOiAnLi90YWlsd2luZC5jb25maWcuY2pzJ1xuICAgICAgICB9KVxuICAgICAgXVxuICAgIH1cbiAgfSxcblxuXG59KVxuIl0sCiAgIm1hcHBpbmdzIjogIjtBQUNBLE9BQU8sZ0JBQWlCO0FBQ3hCLE9BQU8sZ0JBQWdCO0FBRXZCLFNBQVMsb0JBQW9CO0FBQzdCLE9BQU8sV0FBVztBQUVsQixJQUFPLHNCQUFRLGFBQWE7QUFBQSxFQUMxQixTQUFTO0FBQUEsSUFBQyxNQUFNO0FBQUEsRUFFaEI7QUFBQSxFQUVBLEtBQUs7QUFBQSxJQUNILFNBQVM7QUFBQSxNQUNQLFNBQVM7QUFBQSxRQUNQLFdBQVc7QUFBQSxRQUNYLFdBQVc7QUFBQSxVQUNULFFBQVE7QUFBQSxRQUNWLENBQUM7QUFBQSxNQUNIO0FBQUEsSUFDRjtBQUFBLEVBQ0Y7QUFHRixDQUFDOyIsCiAgIm5hbWVzIjogW10KfQo=
