import { describe, expect, it } from 'vitest';
import * as src from '@/utils/params';

describe('Params utility tests', () => {
	describe('Object to Search Params', () => {
		it('should convert a simple object to a URL query string', () => {
			// Arrange
			const obj = { key1: 'value1', key2: 'value2' };

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('key1=value1&key2=value2');
		});

		it('should handle empty objects', () => {
			// Arrange
			const obj = {};

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('');
		});

		it('should encode special characters', () => {
			// Arrange
			const obj = { key: 'value with spaces', special: 'chars&symbols' };

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('key=value+with+spaces&special=chars%26symbols');
		});

		it('should handle null and undefined values', () => {
			// Arrange
			const obj = { key1: null, key2: undefined, key3: 'value3' };

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('key3=value3');
		});

		it('should handle boolean values', () => {
			// Arrange
			const obj = { key1: true, key2: false, key3: 'value3' };

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('key1=true&key2=false&key3=value3');
		});

		it('should handle arrays as values', () => {
			// Arrange
			const obj = { key: ['value1', 'value2'] };

			// Act
			const url = src.objectToSearchParams(obj);

			// Assert
			expect(url).toBe('key=value1%2Cvalue2');
		});
	});
});
