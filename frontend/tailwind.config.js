/** @type {import('tailwindcss').Config} */
export default {
	content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
	theme: {
		extend: {
			colors: {
				'irkut-light': '#3555AE',
				'irkut-dark': '#1A2D67',
				'ap-black': '#333333',
				'ap-gray': '#B8B8B8',
				'ap-dark-gray': '#777777',
				'ap-light-gray': '#ECECEC',
				'ap-white': '#FFFFFF',
				'ap-blue': '#5C98D0',
				'ap-dark-blue': '#072845',
				'ap-green': '#54C16C',
				'ap-dark-green': '#072845',
				'ap-yellow': '#EBB855',
				'ap-dark-yellow': '#5A3F0B',
				'ap-red': '#E25E63',
				'ap-dark-red': '#540E10',
				'ap-sky-blue': '#5DC7DE',
				'ap-dark-sky-blue': '#094B59',
				'ap-lime': '#AAC954',
				'ap-dark-lime': '#3E5107',
			},
		},
	},
	plugins: [],
}
