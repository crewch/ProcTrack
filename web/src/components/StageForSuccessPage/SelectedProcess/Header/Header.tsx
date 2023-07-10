import { Box } from '@mui/material'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/HeaderStyles/Header.module.scss'
import HeaderField from '../../../MainPage/SelectedProcess/InfoProcess/HeaderProcessField/HeaderField'
import UserField from '../../../MainPage/SelectedProcess/InfoProcess/UserField/UserField'
import FilesField from '../../../MainPage/SelectedProcess/InfoProcess/FilesField/FilesField'

const Header = () => {
	return (
		<Box className={styles.container}>
			<Box>
				<HeaderField
					name='Второй процесс'
					status={'в процессе'}
					importance={'Средняя важность'}
					type={'Первый тип'}
				/>
			</Box>
			<Box>
				<UserField
					group={'группа выпускающего'}
					responsible={'Иван Иванов'}
					role={'Ответственный'}
				/>
			</Box>
			<FilesField processId={0} />
		</Box>
	)
}

export default Header
