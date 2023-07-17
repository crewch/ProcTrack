import { FC } from 'react'
import { IListImg } from '../../../../../interfaces/IMainPage/IContainerListProcess/IListProcess/IListImg/IListImg'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/ListProcessStyles/ListProcess.module.scss'

const ListImg: FC<IListImg> = ({ status }) => {
	return (
		<>
			{status === 'в процессе' && (
				<img src='/inprogress.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'завершен' && (
				<img src='/completed.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'остановлен' && (
				<img src='/pause-circle.svg' className={styles.img} loading='lazy' />
			)}
			{status === 'отменен' && (
				<img src='/rejected.svg' className={styles.img} loading='lazy' />
			)}
		</>
	)
}

export default ListImg
