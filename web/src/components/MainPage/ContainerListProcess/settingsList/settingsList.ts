import { ISettingsList } from '../../../../interfaces/IMainPage/IContainerListProcess/ISearch/ISettingsList'

export const settingsList: ISettingsList[] = [
	{
		mainSetting: { type: 'Важность', status: false },
		otherSettings: [
			{ type: 'Высокая важность', status: false },
			{ type: 'Средняя важность', status: false },
			{ type: 'Низкая важность', status: false },
		],
	},
	{
		mainSetting: { type: 'Тип', status: false },
		otherSettings: [{ type: 'КД', status: false }],
	},
	{
		mainSetting: { type: 'Статус', status: false },
		otherSettings: [
			{ type: 'в процессе', status: false },
			{ type: 'завершен', status: false },
			{ type: 'остановлен', status: false },
			{ type: 'отменен', status: false },
		],
	},
]
