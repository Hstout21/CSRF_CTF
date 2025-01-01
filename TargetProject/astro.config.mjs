import { defineConfig } from 'astro/config';
import tailwind from '@astrojs/tailwind';
import node from '@astrojs/node';

export default defineConfig({
	output: 'server',
	security: { checkOrigin: true },
	adapter: node({ mode: 'standalone' }),
    integrations: [tailwind()],
});