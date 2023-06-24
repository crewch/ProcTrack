import { ISettingsList } from '../../../../interfaces/IMainPage/IContainerListProcess/ISettingsList'

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
		otherSettings: [
			{ type: 'Первый тип', status: false },
			{ type: 'Второй тип', status: false },
		],
	},
	{
		mainSetting: { type: 'Статус', status: false },
		otherSettings: [
			{ type: 'В процессе', status: false },
			{ type: 'Заврешено', status: false },
		],
	},
]
