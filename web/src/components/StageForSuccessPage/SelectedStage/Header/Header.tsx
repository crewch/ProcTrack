import { Box, Button } from '@mui/material'
import HeaderField from '../../../MainPage/SelectedStage/HeaderField/HeaderField'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedStageStyles/HeaderStyles/Header.module.scss'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'

const Header = () => {
	return (
		<Box className={styles.container}>
			<Box className={styles.headerField}>
				<HeaderField
					name={'Исправление 1'}
					status={'Отправлен на проверку'}
					nameOfGroup={'Отдел управления технологичностью изделия'}
				/>
			</Box>
			<Box>
				<UserField
					group={'Отдел управления технологичностью изделия'}
					responsible={'Вещак Валерий Георгиевич'}
					role={'Главный согласующий'}
				/>
			</Box>
			<Button variant='outlined'>Отменить утверждение</Button>
		</Box>
	)
}

export default Header
