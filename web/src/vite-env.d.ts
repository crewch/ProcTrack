/// <reference types="vite/client" />
/// <reference types="vite-plugin-svgr/client" />

interface ImportMetaEnv {
	readonly VITE_API_HOST: string
	readonly VITE_API_PORT: string
	// more env variables...
}

interface ImportMeta {
	readonly env: ImportMetaEnv
}
