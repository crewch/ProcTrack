export interface ISetting {
	type: string
	status: boolean
}

export interface ISettingsList {
	mainSetting: ISetting
	otherSettings: ISetting[]
}
