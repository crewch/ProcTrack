import { Box } from '@mui/material'
import DateInfo from '../../../MainPage/SelectedProcess/InfoProcess/DateInfoField/DateInfo'
import styles from '/src/styles/StageForSuccessPageStyles/SelectedProcessStyles/FooterStyles/Footer.module.scss'

const Footer = () => {
	return (
		<Box className={styles.container}>
			<DateInfo
				page='stageForSuccess'
				startDate='пт, 22 декабря 2023, 16:30'
				interval='3 дня 2 часа 11 минут'
				endData='чт, 28 декабря 2023, 12:00'
			/>
		</Box>
	)
}

export default Footer
