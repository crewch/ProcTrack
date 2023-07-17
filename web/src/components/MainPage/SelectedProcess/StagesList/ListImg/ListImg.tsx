import { FC } from 'react'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'
import { IListImg } from '../../../../../interfaces/IMainPage/IContainerListProcess/IListProcess/IListImg/IListImg'

const ListImg: FC<IListImg> = ({ status }) => {
	return (
		<>
			{status === 'Согласовано' && (
				<img src='/completed.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Не начат' && (
				<img src='/stoppedProcess.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Согласовано-Блокировано' && (
				<img src='/lock.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Принят на проверку' && (
				<img
					src='/arrow-circle-down.svg'
					loading='lazy'
					className={styles.img}
				/>
			)}
			{status === 'Отправлен на проверку' && (
				<img
					src='/arrow-circle-right.svg'
					loading='lazy'
					className={styles.img}
				/>
			)}
			{status === 'Отменен' && (
				<img src='/rejected.svg' loading='lazy' className={styles.img} />
			)}
			{status === 'Остановлен' && (
				<img src='/pause-circle.svg' loading='lazy' className={styles.img} />
			)}
		</>
	)
}

export default ListImg
