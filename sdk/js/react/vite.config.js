/// <reference types="vitest/config" />
import { defineConfig } from 'vite';
import viteTsConfigPaths from 'vite-tsconfig-paths';

const config = defineConfig({
	plugins: [
		viteTsConfigPaths({
			projects: ['./tsconfig.json'],
		}),
	],
});

export default config;
