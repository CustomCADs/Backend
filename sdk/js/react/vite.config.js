/// <reference types="vitest/config" />
import { defineConfig } from 'vite';
import viteTsConfigPaths from 'vite-tsconfig-paths';
import alias from '@rollup/plugin-alias';
import path from 'path';

const config = defineConfig({
	plugins: [
		alias({
			entries: [
				{ find: '@', replacement: path.resolve(__dirname, 'src') },
			],
		}),
		viteTsConfigPaths({
			projects: ['./tsconfig.json'],
		}),
	],
});

export default config;
