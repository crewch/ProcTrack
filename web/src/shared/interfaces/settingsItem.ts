interface Setting {
	type: string
	status: boolean
}

export interface SettingsItem {
	mainSetting: Setting
	otherSettings: Setting[]
}
